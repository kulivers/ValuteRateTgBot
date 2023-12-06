namespace Telegram.PriceCalculator.Calculator;

public class FormulaCalculationManager
{
    private FormulaCalculationService _calculationService;

    public FormulaCalculationManager(FormulaCalculationService calculationService)
    {
        _calculationService = calculationService;
    }


}
