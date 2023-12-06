using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Contracts;

public interface IUserFormulaRepository
{
    Task<UserFormula> CreateAsync(UserFormula formula);
    UserFormula Create(UserFormula formula);
    UserFormula Edit(UserFormula formula);
    UserFormula Delete(UserFormula formula);
    UserFormula? Get(long id);
}
