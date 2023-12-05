﻿using System.Collections.Concurrent;
using CentralBankSDK;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.PriceCalculator.Calculator.Api;

namespace Telegram.PriceCalculator.Calculator;

public class ValuteRateProvider : BackgroundService, IValuteRateProvider
{
    private readonly ILogger<ValuteRateProvider> _logger;

    private TimeSpan _interval;
    private readonly ConcurrentDictionary<string, ValuteCursOnDateDto> _currentRate;
    private readonly TimeSpan _defaultInterval = TimeSpan.FromHours(2);

    private readonly ICentralBankService _centralBankService;

    public ValuteRateProvider(ILogger<ValuteRateProvider> logger, ICentralBankService centralBankService)
    {
        _logger = logger;
        _interval = _defaultInterval;
        _centralBankService = centralBankService;
        _currentRate = new ConcurrentDictionary<string, ValuteCursOnDateDto>();
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

    public ValuteCursOnDateDto GetCurrentRate(string vchCode)
    {
        return _currentRate[vchCode];
    }

    public ConcurrentDictionary<string, ValuteCursOnDateDto> GetCurrentRate()
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