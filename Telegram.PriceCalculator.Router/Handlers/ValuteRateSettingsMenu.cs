using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu;

public class ValuteRateSettingsMenu : ActionHandler
{
    public static string ActionName => ActionNames.Menu.ValuteRateSettings;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update)
    {
        var actions = new List<string>()
        {
            ActionNames.ValuteRateSettings.UpdateRates,
            ActionNames.ValuteRateSettings.GetByCountry,
            ActionNames.ValuteRateSettings.GetByVch,
            ActionNames.ValuteRateSettings.GetAllVch,
        }.Select(i=>new KeyValuePair<string, string>(i, i));

        userContext.Set(update.Message.From.Id, Routes.Valute.Root);
        await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: "Choose action",
            replyMarkup: TgViewsFactory.GetInlineKeyboard(actions, (byte)actions.Count(), 1),
            cancellationToken: CancellationToken.None);

    }
}
