using Telegram.PriceCalculator.Contracts;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Repository;

public class VariablesRepository : RepositoryBase<Variable>, IVariablesRepository
{
    public VariablesRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void Delete(Variable variable)
    {
        base.Delete(variable);
    }

    public void DeleteRange(IEnumerable<Variable> variables)
    {
        foreach (var variable in variables)
        {
            base.Delete(variable);
        }
    }
}

public class UserFormulaRepository : RepositoryBase<UserFormula>, IUserFormulaRepository
{
    private readonly RepositoryContext _repositoryContext;

    public UserFormulaRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public UserFormula Edit(UserFormula formula)
    {
        base.Update(formula);
        return formula;
    }

    public UserFormula Delete(UserFormula formula)
    {
        base.Delete(formula);
        return formula;
    }

    public async Task<UserFormula> CreateAsync(UserFormula formula)
    {
        await base.CreateAsync(formula);
        return formula;
    }

    public UserFormula? Get(long id)
    {
        return base.FindByCondition(formula => formula.FormulaId == id).SingleOrDefault();
    }

    public UserFormula? GetByUserId(long id)
    {
        return base.FindByCondition(formula => formula.UserId == id).SingleOrDefault();
    }

    public UserFormula Create(UserFormula formula)
    {
        base.Create(formula);
        return formula;
    }
}
