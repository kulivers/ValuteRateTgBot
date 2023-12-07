using System.Collections.Concurrent;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Calculator.Api;

public interface IValuteRateProvider
{
    Task UpdateRate();
    void SetInterval(TimeSpan interval);
    ValuteCursOnDateDto? GetCurrentRate(string vchCode);
    bool TryGetCurrentRate(string vchCode, out ValuteCursOnDateDto? result);
    ConcurrentDictionary<string, ValuteCursOnDateDto?> GetCurrentRate();
    public IEnumerable<string> GetVchCodes();
}
