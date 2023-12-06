using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Contracts;
using Telegram.PriceCalculator.Repository;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Services;

public class FormulaCalculationManager : IFormulaCalculationManager
{
    private readonly FormulaCalculator _calculator;
    private readonly IRepositoryManager _repository;
    private readonly IValuteRateProvider _valuteRateProvider;

    public FormulaCalculationManager(IRepositoryManager repository, IValuteRateProvider valuteRateProvider)
    {
        _calculator = new FormulaCalculator();
        _repository = repository;
        _valuteRateProvider = valuteRateProvider;
    }

    public async Task Create(UserFormula formula)
    {
        await _repository.Formulas.CreateAsync(formula);
        await _repository.SaveAsync();
    }

    public async Task Edit(UserFormula formula)
    {
        _repository.Formulas.Edit(formula);
        await _repository.SaveAsync();
    }

    public async Task Delete(UserFormula formula)
    {
        _repository.Formulas.Delete(formula);
        await _repository.SaveAsync();
    }

    public UserFormula? Get(long id)
    {
        return _repository.Formulas.Get(id);
    }

    public TestFormulaResult TestFormula(long id)
    {
        var formula = Get(id);
        if (formula == default)
        {
            return new TestFormulaResult(false, $"No formula by this id {id}");
        }

        try
        {
            _calculator.Calculate(formula);
            return new TestFormulaResult(true, "");
        }
        catch (Exception e)
        {
            return new TestFormulaResult(false, e.Message);
        }
    }

    public async Task AddVariables(long formulaId, IEnumerable<Variable> variables)
    {
        var formula = Get(formulaId) ?? throw new Exception("Trying to get access to not existing formula");
        if (formula.Variables != null && formula.Variables.Any())
        {
            formula.Variables.AddRange(variables);
        }
        else
        {
            formula.Variables = variables.ToList();
        }

        await Edit(formula);
    }

    public async Task DeleteVariables(long formulaId, IEnumerable<Variable> toDelete)
    {
        var formula = Get(formulaId) ?? throw new Exception("Trying to get access to not existing formula");
        var toDeleteIds = toDelete.Select(v => v.Id).ToHashSet();
        if (formula.Variables != null && formula.Variables.Any())
        {
            formula.Variables = formula.Variables.Where(variable => !toDeleteIds.Contains(variable.Id)).ToList();
        }
        else
        {
            return;
        }

        await Edit(formula);
    }

    public bool TryCalculateResult(UserFormula formula, out decimal result)
    {
        formula.Variables = RefreshValuteVariables(formula.Variables);

        try
        {
            result = _calculator.Calculate(formula);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    private List<Variable>? RefreshValuteVariables(List<Variable>? variables)
    {
        if (variables == null)
        {
            return null;
        }

        var refreshedVariables = new List<Variable>();
        foreach (var variable in variables)
        {
            if (variable is ValuteCalculatedVariable valuteVar)
            {
                var vchCode = valuteVar.VchCode;
                var currentRate = _valuteRateProvider.GetCurrentRate(vchCode);
                valuteVar.Value = currentRate.Vcurs;
                refreshedVariables.Add(valuteVar);
            }
            else
            {
                refreshedVariables.Add(variable);
            }
        }

        return refreshedVariables;
    }

}