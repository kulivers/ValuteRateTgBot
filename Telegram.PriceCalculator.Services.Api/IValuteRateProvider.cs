using System.Collections.Concurrent;

namespace Telegram.PriceCalculator.Calculator.Api;

public interface IValuteRateProvider
{
    Task UpdateRate();
    void SetInterval(TimeSpan interval);
    Telegram.PriceCalculator.Shared.ValuteCursOnDateDto GetCurrentRate(string vchCode);
    ConcurrentDictionary<string, Telegram.PriceCalculator.Shared.ValuteCursOnDateDto> GetCurrentRate();
}
