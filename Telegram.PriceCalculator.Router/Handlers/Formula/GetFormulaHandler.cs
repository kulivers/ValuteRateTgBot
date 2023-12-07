using Telegram.Bot;
using Telegram.PriceCalculator.Services;

namespace Telegram.PriceCalculator.Router.Menu.Formula;

public class DeleteFormulaHandler : IActionHandler
{
    private IFormulaCalculationManager _manager;

    public DeleteFormulaHandler(IFormulaCalculationManager manager)
    {
        _manager = manager;
    }
    public string ActionName => ActionNames.FormulaSettings.DeleteFormula;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        var formula = _manager.GetByUserId(userId);
        if (formula == default)
        {
            return;
        }

        await _manager.Delete(formula);
    }
}
