using org.matheval;
using Telegram.PriceCalculator.Calculator.Api;

namespace Telegram.PriceCalculator.Calculator;

public class FormulaCalculationService : IFormulaCalculationService
{
    public decimal Calculate(string formula, params KeyValuePair<string, decimal>[] variables)
    {
        var expression = new Expression(formula);
        foreach (var (varName, varValue) in variables)
        {
            expression.Bind(varName, varValue);
        }

        var value = expression.Eval();
        return (decimal)value;
    }
}

