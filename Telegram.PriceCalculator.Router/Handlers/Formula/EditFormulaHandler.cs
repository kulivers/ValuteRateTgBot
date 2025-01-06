using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.PriceCalculator.Router.Menu.Formula;

public class EditFormulaHandler : ActionHandler
{
    public override string ActionName => ActionNames.FormulaSettings.EditFormula;

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
