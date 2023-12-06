namespace Telegram.PriceCalculator.Contracts;

public interface IRepositoryManager
{
    IUserFormulaRepository Formulas { get; }
    void Save();
    Task SaveAsync();
}
