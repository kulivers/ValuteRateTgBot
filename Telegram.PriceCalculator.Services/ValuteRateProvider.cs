using System.Collections.Concurrent;
using CentralBankSDK;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Services;

public class ValuteRateProvider : BackgroundService, IValuteRateProvider //todo remove from api
{
    private readonly ILogger<ValuteRateProvider> _logger;

    private TimeSpan _interval;
    private readonly ConcurrentDictionary<string, ValuteCursOnDateDto?> _currentRate;
    private readonly TimeSpan _defaultInterval = TimeSpan.FromHours(2);

    private readonly ICentralBankService _centralBankService;

    public ValuteRateProvider(ILogger<ValuteRateProvider> logger, ICentralBankService centralBankService)
    {
        _logger = logger;
        _interval = _defaultInterval;
        _centralBankService = centralBankService;
        _currentRate = new ConcurrentDictionary<string, ValuteCursOnDateDto?>();
    }

    public async Task UpdateRate()
    {
        var curRate = await _centralBankService.GetCursOnDateXMLAsync(DateTime.Now);
        var mapper = new Mapper();
        foreach (var rate in curRate)
        {
            _currentRate.AddOrUpdate(rate.VchCode, (key, newRate) => mapper.Map(newRate), (key, newRate2, _) => newRate2, rate);
        }
    }

    public void SetInterval(TimeSpan interval)
    {
        _interval = interval;
    }

    public ValuteCursOnDateDto? GetCurrentRate(string vchCode)
    {
        return _currentRate[vchCode];
    }

    public bool TryGetCurrentRate(string vchCode, out ValuteCursOnDateDto? result)
    {
        return _currentRate.TryGetValue(vchCode.ToUpper(), out result);
    }

    public ConcurrentDictionary<string, ValuteCursOnDateDto?> GetCurrentRate()
    {
        return _currentRate;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting valuteRateProvider service");
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateRate();
            await Task.Delay(_interval);
        }
    }
}
