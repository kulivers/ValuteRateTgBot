using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu;

public class DefaultActionHandler : ActionHandler
{
    public static string ActionName => ActionNames.Default;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update)
    {
        var userMessage = update?.Message?.Text;
        if (string.IsNullOrWhiteSpace(userMessage))
        {
            await ShowMenu(botClient, update.Message, CancellationToken.None);
            return;
        }

        var context = userContext.Get(update.Message.From.Id);
        if (context != RoutesStorageTree.DefaultPath)
        {
            //handle
        }

        if (decimal.TryParse(userMessage, out var result))
        {
            //calculate for user formula //todo here is actions by context
            return;
        }

        await ShowMenu(botClient, update.Message, CancellationToken.None);
    }

    private async Task<Message> ShowMenu(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        return await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "What to do",
            replyMarkup: TgViewsFactory.GetKeyboard(ActionNames.Menu.ValuteRateSettings, ActionNames.Menu.FormulaSettings),
            cancellationToken: cancellationToken);
    }

}
