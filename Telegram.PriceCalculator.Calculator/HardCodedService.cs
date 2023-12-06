using Telegram.PriceCalculator.Calculator.Api;

namespace Telegram.PriceCalculator.Calculator;

public class HardCodedService : IHardcodedCalculationService
{
    private readonly ValuteRateProvider _valuteRateProvider;

    public HardCodedService(ValuteRateProvider provider)
    {
        _valuteRateProvider = provider;
    }

    public decimal Calculate(decimal value, string vchCode)
    {
        var currentRate = _valuteRateProvider.GetCurrentRate(vchCode);
        return currentRate.Vcurs * value;
    }
}
