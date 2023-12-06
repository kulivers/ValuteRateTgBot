using System.Collections.Concurrent;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Calculator.Api;

public interface IValuteRateProvider
{
    Task UpdateRate();
    void SetInterval(TimeSpan interval);
    ValuteCursOnDateDto GetCurrentRate(string vchCode);
    ConcurrentDictionary<string, ValuteCursOnDateDto> GetCurrentRate();
}
