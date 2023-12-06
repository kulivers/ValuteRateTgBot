using System.Reflection.Metadata.Ecma335;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Presentation;

namespace Telegram.PriceCalculator.Router.Menu;

public interface IActionHandler
{
    string ActionName { get; }
    Task Handle(ITelegramBotClient botClient, string userContext, Update update);
}

public class ValuteRateSettingsMenu : IActionHandler
{
    public string ActionName => ActionNames.Menu.ValuteRateSettings;
    public Task Handle(ITelegramBotClient botClient, string userContext, Update update)
    {
        throw new NotImplementedException();
    }
}

public class DefaultActionHandler : IActionHandler
{
    public string ActionName => ActionNames.Default;
    public async Task Handle(ITelegramBotClient botClient, string userContext, Update update)
    {
        var userMessage = update?.Message?.Text;
        if (string.IsNullOrWhiteSpace(userMessage))
        {
            return;
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
            replyMarkup: TgViewsFactory.GetMenu(ActionNames.Menu.ValuteRateSettings, ActionNames.Menu.FormulaSettings),
            cancellationToken: cancellationToken);
    }

}
