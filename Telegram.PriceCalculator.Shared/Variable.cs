using System.ComponentModel.DataAnnotations;

namespace Telegram.PriceCalculator.Shared;

public enum VariableKind
{
    Undefined,
    Simple,
    ValuteCalculatedVariable
}

public record Variable
{
    [Required]
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Value { get; set; }
}

public record ValuteCalculatedVariable : Variable
{
    [Required]
    public string VchCode { get; set; }
}
