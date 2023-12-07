using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Router.Menu;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Handlers.Menus;

public class FormulaSettingsSettingsMenu : ActionHandler
{
    public override string ActionName => ActionNames.Menu.FormulaSettings;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        var actions = new List<string>()
        {
            ActionNames.FormulaSettings.SetupNewFormula,
            ActionNames.FormulaSettings.GetFormula,
            ActionNames.FormulaSettings.ListFormulas,
            ActionNames.FormulaSettings.EditFormula,
        }.Select(i=>new KeyValuePair<string, string>(i, i));

        userContext.Set(userId, Routes.Formula.Root);//todo everywhere is ctx
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Choose action",
            replyMarkup: TgViewsFactory.GetInlineKeyboard(actions, (byte)actions.Count(), 1),
            cancellationToken: CancellationToken.None);
    }
}
