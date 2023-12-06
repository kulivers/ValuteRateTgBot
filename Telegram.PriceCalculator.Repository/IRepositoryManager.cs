namespace Telegram.PriceCalculator.Repository;

public interface IRepositoryManager
{
    IUserFormulaRepository Formulas { get; }
    void Save();
}
