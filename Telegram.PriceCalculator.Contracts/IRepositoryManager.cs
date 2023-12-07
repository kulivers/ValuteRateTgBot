namespace Telegram.PriceCalculator.Contracts;

public interface IRepositoryManager
{
    IVariablesRepository Variables { get; }
    IUserFormulaRepository Formulas { get; }
    void Save();
    Task SaveAsync();
}
