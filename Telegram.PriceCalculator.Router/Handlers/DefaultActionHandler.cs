using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu;

public class DefaultActionHandler : ActionHandler
{
    public override string ActionName => ActionNames.Default;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            await ShowMenu(botClient, chatId, token);
            return;
        }

        var context = userContext.Get(userId);
        if (context != RoutesStorageTree.DefaultPath)
        {
            //handle
        }

        if (decimal.TryParse(message, out var result))
        {
            //calculate for user formula //todo here is actions by context
            return;
        }

        await ShowMenu(botClient, chatId, CancellationToken.None);
    }

    private async Task<Message> ShowMenu(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        return await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "No handler for this action.",
            replyMarkup: TgViewsFactory.GetKeyboard(ActionNames.Menu.ValuteRateSettings, ActionNames.Menu.FormulaSettings),
            cancellationToken: cancellationToken);
    }

}
