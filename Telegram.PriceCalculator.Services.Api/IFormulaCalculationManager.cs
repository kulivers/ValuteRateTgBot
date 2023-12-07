using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Services;

public interface IFormulaCalculationManager
{
    Task Create(string message, long userId);
    Task Edit(UserFormula formula);
    Task Delete(UserFormula formula);
    UserFormula? Get(long id);
    UserFormula? GetByUserId(long id);
    TestFormulaResult TestFormula(long id);
    bool TryCalculateResult(UserFormula formula, out decimal result);
    Task AddVariables(long formulaId, IEnumerable<Variable> variables);
    Task DeleteVariables(long formulaId, IEnumerable<Variable> toDelete);
}
