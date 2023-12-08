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
        if (_repository.Formulas.GetByUserId(formula.UserId) != null)
        {
            throw new InvalidOperationException();
        }

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
        if (formula.Variables!=null)
        {
            _repository.Variables.DeleteRange(formula.Variables);
        }
        await _repository.SaveAsync();

        _repository.Formulas.Delete(formula);
        await _repository.SaveAsync();
    }

    public UserFormula? Get(long id)
    {
        return _repository.Formulas.Get(id);
    }
    public UserFormula? GetByUserId(long id)
    {
        var formulaId = _repository.Formulas.GetByUserId(id);//todo fix
        return formulaId;
    }

    public bool TestFormula(UserFormula formula)
    {
        if (formula == default)
        {
            return false;
        }

        for (var i = -5; i < 5; i++)
        {
            if (TryCalculateResult(formula, i, out _))
            {
                return true;
            }
        }

        return false;
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

    public async Task<bool> Create(string message, long userId)
    {
        var rows = message.Split('\n');
        var formula = rows[0];
        var variablesRows = rows.Skip(1).ToArray();
        var variables = new List<Variable>();
        foreach (var row in variablesRows)
        {
            var split = row.Split(' ');
            variables.Add(new Variable(){Value = decimal.Parse(split[1]), Name = split[0]});
        }

        foreach (var vchCode in _valuteRateProvider.GetVchCodes())
        {
            var upper = vchCode.ToUpper();
            if (!formula.Contains(upper) && !formula.Contains(vchCode))
            {
                continue;
            }

            variables.Add(new ValuteCalculatedVariable() { Name = upper, VchCode = upper, Value = _valuteRateProvider.GetCurrentRate(upper)!.Vcurs });
        }

        var userFormula = new UserFormula(){Variables = variables, Formula = formula, UserId = userId};
        var successTest = TestFormula(userFormula);
        if (!successTest)
        {
            return false;
        }

        await _repository.Formulas.CreateAsync(userFormula);
        await _repository.SaveAsync();
        return true;
    }

    public bool TryCalculateResult(UserFormula formula, decimal userValue, out decimal result)
    {
        try
        {
            formula.Variables ??= new List<Variable>();
            if (!formula.Variables.Exists(v=>v.Name == "USER"))
            {
                formula.Variables.Add(new Variable() { Name = "USER", Value = userValue });
            }
            formula.Variables = RefreshValuteVariables(formula.Variables);
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
