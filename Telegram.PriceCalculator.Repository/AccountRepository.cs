using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Repository;

public class UserFormulaRepository : RepositoryBase<UserFormula>, IUserFormulaRepository
{
    private readonly RepositoryContext _repositoryContext;

    public UserFormulaRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
}
