using System.Text;
using Telegram.Bot;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class GetAllVchHandler : IActionHandler
{
    private readonly IValuteRateProvider _rateProvider;
    private const int MaxTgMessageLength = 4096;

    public GetAllVchHandler(IValuteRateProvider rateProvider)
    {
        _rateProvider = rateProvider;
    }
    // ActionNames.ValuteRateSettings.GetAllVch,
    public string ActionName => ActionNames.ValuteRateSettings.GetAllVch;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Valute.GetAllRates);
        var rates = _rateProvider.GetCurrentRate();
        var builder = new StringBuilder();
        builder.Append("[VCH] - [Valute name] - [Current rate]\n");
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
}
