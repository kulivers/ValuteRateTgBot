namespace Telegram.PriceCalculator.Services;

public class FormulaCalculationManager
{
    private FormulaCalculationService _calculationService;

    public FormulaCalculationManager(FormulaCalculationService calculationService)
    {
        _calculationService = calculationService;
    }


}
