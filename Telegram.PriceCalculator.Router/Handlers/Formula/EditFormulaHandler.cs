using Telegram.Bot;

namespace Telegram.PriceCalculator.Router.Menu.Formula;

public class EditFormulaHandler : IActionHandler
{
    public string ActionName => ActionNames.FormulaSettings.EditFormula;

    public Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
