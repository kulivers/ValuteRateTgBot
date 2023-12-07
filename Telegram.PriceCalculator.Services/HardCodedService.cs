using Telegram.PriceCalculator.Calculator.Api;

namespace Telegram.PriceCalculator.Services;

public class HardCodedService// : IHardcodedCalculationService todo delete
{
    private readonly IValuteRateProvider _valuteRateProvider;

    public HardCodedService(IValuteRateProvider provider)
    {
        _valuteRateProvider = provider;
    }

    public decimal Calculate(decimal value, string vchCode)
    {
        var currentRate = _valuteRateProvider.GetCurrentRate(vchCode);
        return currentRate.Vcurs * value;
    }
}
