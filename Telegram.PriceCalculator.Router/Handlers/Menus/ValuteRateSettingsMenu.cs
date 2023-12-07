using Telegram.Bot;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Router.Menu;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Handlers.Menus;

public class ValuteRateSettingsMenu : IActionHandler
{
    public string ActionName => ActionNames.Menu.ValuteRateSettings;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        var actions = new List<string>()
        {
            ActionNames.ValuteRateSettings.UpdateRates,
            ActionNames.ValuteRateSettings.GetByVchInfo,
            ActionNames.ValuteRateSettings.GetAllVch,
        }.Select(i=>new KeyValuePair<string, string>(i, i));

        userContext.Set(userId, Routes.Valute.Root);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Choose action",
            replyMarkup: TgViewsFactory.GetInlineKeyboard(actions, (byte)actions.Count(), 1),
            cancellationToken: token);

    }
}
