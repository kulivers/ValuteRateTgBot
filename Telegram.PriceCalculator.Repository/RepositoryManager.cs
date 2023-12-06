using Telegram.PriceCalculator.Contracts;

namespace Telegram.PriceCalculator.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserFormulaRepository> _userFormulasRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _userFormulasRepository = new Lazy<IUserFormulaRepository>(() => new UserFormulaRepository(repositoryContext));
    }

    public IUserFormulaRepository Formulas => _userFormulasRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
}
