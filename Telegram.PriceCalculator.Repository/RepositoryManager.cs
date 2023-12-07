using Telegram.PriceCalculator.Contracts;

namespace Telegram.PriceCalculator.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserFormulaRepository> _userFormulasRepository;
    private readonly Lazy<VariablesRepository> _variablesRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _userFormulasRepository = new Lazy<IUserFormulaRepository>(() => new UserFormulaRepository(repositoryContext));
        _variablesRepository = new Lazy<VariablesRepository>(() => new VariablesRepository(repositoryContext));
    }

    public IUserFormulaRepository Formulas => _userFormulasRepository.Value;
    public IVariablesRepository Variables => _variablesRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
