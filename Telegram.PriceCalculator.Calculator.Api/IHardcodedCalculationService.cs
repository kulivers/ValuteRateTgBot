namespace Telegram.PriceCalculator.Calculator.Api;

public interface IHardcodedCalculationService
{
    decimal Calculate(decimal value, string vchCode);
}
