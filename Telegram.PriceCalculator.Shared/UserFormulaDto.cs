namespace Telegram.PriceCalculator.Shared;

public record UserFormulaDto
{
    public UserFormulaDto()
    {

    }

    public UserFormulaDto(long userId, string formulaId, string formula, List<Variable> variables)
    {
        UserId = userId;
        FormulaId = formulaId;
        Formula = formula;
        Variables = variables;
    }

    public long UserId { get; set; }
    public string FormulaId { get; set; }
    public string Formula { get; set; }

    public List<Variable> Variables { get; set; }
}

public record ValuteCalculatedVariable : Variable
{
    public string VchCode { get; set; }
    public DateTime LastUpdateTime { get; set; }
}

public record Variable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}
