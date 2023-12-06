namespace Telegram.PriceCalculator.Calculator.Api;

public interface IFormulaCalculationService
{
    decimal Calculate(string formula, params KeyValuePair<string, decimal>[] variables);
}
