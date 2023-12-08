using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Services;

public interface IFormulaCalculationManager
{
    Task<bool> Create(string message, long userId);
    Task Edit(UserFormula formula);
    Task Delete(UserFormula formula);
    UserFormula? Get(long id);
    UserFormula? GetByUserId(long id);
    bool TestFormula(UserFormula id);
    bool TryCalculateResult(UserFormula formula, decimal userValue, out decimal result);
    Task AddVariables(long formulaId, IEnumerable<Variable> variables);
    Task DeleteVariables(long formulaId, IEnumerable<Variable> toDelete);
}
