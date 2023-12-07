using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu;

public class FormulaSettingsSettingsMenu : ActionHandler
{
    public static string ActionName => ActionNames.Menu.FormulaSettings;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update)
    {
        var actions = new List<string>()
        {
            ActionNames.FormulaSettings.SetupNewFormula,
            ActionNames.FormulaSettings.GetFormula,
            ActionNames.FormulaSettings.ListFormulas,
            ActionNames.FormulaSettings.EditFormula,
        }.Select(i=>new KeyValuePair<string, string>(i, i));

        userContext.Set(update.Message.From.Id, Routes.Formula.Root);
        await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: "Choose action",
            replyMarkup: TgViewsFactory.GetInlineKeyboard(actions, (byte)actions.Count(), 1),
            cancellationToken: CancellationToken.None);

    }
}
