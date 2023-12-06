using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telegram.PriceCalculator.Shared;

public class TestFormulaResult
{
    public TestFormulaResult()
    {

    }

    public TestFormulaResult(bool success, string exceptionMessage)
    {
        Success = success;
        ExceptionMessage = exceptionMessage;
    }

    public bool Success { get; set; }
    public string ExceptionMessage { get; set; }
}

public class UserFormula
{
    public UserFormula()
    {

    }

    public UserFormula(long formulaId, long userId, string formula, List<Variable> variables)
    {
        UserId = userId;
        FormulaId = formulaId;
        Formula = formula;
        Variables = variables;
    }
    public UserFormula(long formulaId, long userId, string formula)
    {
        UserId = userId;
        FormulaId = formulaId;
        Formula = formula;
    }

    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long FormulaId { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public string Formula { get; set; }
    public List<Variable>? Variables { get; set; }
}

