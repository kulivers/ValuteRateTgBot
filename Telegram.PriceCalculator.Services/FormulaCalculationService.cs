using org.matheval;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Services;

public class FormulaCalculator
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

    public decimal Calculate(UserFormula formula)
    {
        var expression = new Expression(formula.Formula);
        foreach (var variable in formula.Variables)
        {
            var varValue = variable.Value;
            var varName = variable.Name;
            expression.Bind(varName, varValue);
        }

        var value = expression.Eval();
        return (decimal)value;
    }
}

