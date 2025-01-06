using System.Collections.Concurrent;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class GetAllVchHandler : ActionHandler
{
    private readonly IValuteRateProvider _rateProvider;
    private const int MaxTgMessageLength = 4096;

    public GetAllVchHandler(IValuteRateProvider rateProvider)
    {
        _rateProvider = rateProvider;
    }
    // ActionNames.ValuteRateSettings.GetAllVch,
    public override string ActionName => ActionNames.ValuteRateSettings.GetAllVch;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Valute.GetAllRates);
        var rates = _rateProvider.GetCurrentRate();
        var builder = new StringBuilder();
        builder.Append("[VCH] - [Valute name]\n");
        foreach (var (vch, curseValue) in rates)
        {
            var row = $"{vch} {curseValue.Vname}\n";
            if (builder.Length + row.Length > MaxTgMessageLength)
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: builder.ToString(),
                    cancellationToken: token);
                builder.Clear();
            }

            builder.Append(row);
        }

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: builder.ToString(),
            cancellationToken: token);
    }

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
