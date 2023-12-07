using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Router.Handlers.Menus;
using Telegram.PriceCalculator.Router.Menu;
using Telegram.PriceCalculator.Router.Menu.Valute;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router;

public class MessageRouter
{
    private readonly ILogger<MessageRouter> _logger;
    private readonly ITelegramBotClient _botClient;
    private UserContext _userContext;
    private RoutesStorageTree _routes;
    private readonly Dictionary<string, IActionHandler> _actionHandlers;

    public MessageRouter(ILogger<MessageRouter> logger, ITelegramBotClient botClient, UserContext userContext, RoutesStorageTree routes, IEnumerable<IActionHandler> actionHandlers)
    {
        _logger = logger;
        _botClient = botClient;
        _userContext = userContext;
        _routes = routes;
        _actionHandlers = actionHandlers.ToDictionary(handler => handler.ActionName, handler => handler); //todo perfomance
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var userData = GetUpdateData(update);
        var userId = userData.UserId;
        var chatId = userData.ChatId;
        var messageText = userData.MessageText;

        if (userId == null)
        {
            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Error. Unable get user id.",
                cancellationToken: cancellationToken);
        }

        if (messageText == null || chatId == null || userId == null)
        {
            // await botClient.SendTextMessageAsync(
            //     chatId: chatId,
            //     text: "Error", //error data handler
            //     cancellationToken: cancellationToken);
            return;
        }

        var context = _userContext.Get((long)userId);
        switch (context)
        {
            case Routes.Valute.GetRateVch:
                await _actionHandlers[ActionNames.ValuteRateSettings.GetByVch].Handle(botClient, _userContext, messageText, (long)userId, (long)chatId, cancellationToken);
                return;
            case Routes.Formula.Formulacreate:
                await _actionHandlers[ActionNames.FormulaSettings.SetupNewFormulaInput].Handle(botClient, _userContext, messageText, (long)userId, (long)chatId, cancellationToken);
                return;
        }

        if (_actionHandlers.TryGetValue(messageText, out var actionHandler))
        {
            await actionHandler.Handle(botClient, _userContext, messageText, (long)userId, (long)chatId, cancellationToken);
            return;
        }

        await _actionHandlers[ActionNames.Default].Handle(botClient, _userContext, messageText, (long)userId, (long)chatId, cancellationToken);
    }

    private UpdateData GetUpdateData(Update update)
    {
        long? userId = null;
        long? chatId = null;
        string? messageText = default;
        switch (update)
        {
            case { Message: { } message }:
                userId = message.From?.Id;
                chatId = message.Chat.Id;
                messageText = message.Text;
                break;
            case { EditedMessage: { } message }:
                userId = message.From?.Id;
                chatId = message.Chat.Id;
                messageText = message.Text;
                break;
            case { CallbackQuery: { } callbackQuery }:
                userId = callbackQuery.From.Id;
                chatId = callbackQuery.Message?.Chat.Id;
                messageText = callbackQuery.Data;
                break;
            // case { InlineQuery: { } inlineQuery }:
            //     userId = inlineQuery.From.Id;
            //     chatId = inlineQuery..Id;??
            //     break;
            // case { ChosenInlineResult: { } chosenInlineResult }:
            //     userId = chosenInlineResult.From.Id;
            //     chatId = chosenInlineResult.Chat.Id;
            //     break;
            default:
                break;
        }

        return new UpdateData(userId, chatId, messageText);
    }
}

internal record UpdateData
{
    public long? UserId { get; init; }
    public long? ChatId { get; init; }
    public string? MessageText { get; init; }

    public UpdateData(long? userId, long? chatId, string? messageText)
    {
        UserId = userId;
        ChatId = chatId;
        MessageText = messageText;
    }
}
