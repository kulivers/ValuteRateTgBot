using System.Collections.Concurrent;

namespace Telegram.PriceCalculator.Calculator.Api;

public interface IValuteRateProvider
{
    Task UpdateRate();
    void SetInterval(TimeSpan interval);
    ValuteCursOnDateDto GetCurrentRate(string vchCode);
    ConcurrentDictionary<string, ValuteCursOnDateDto> GetCurrentRate();
}
