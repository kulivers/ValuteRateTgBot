namespace Telegram.PriceCalculator.Calculator.Api;

public interface ICalculationService
{
    decimal Calculate(decimal value, string vchCode);
}
