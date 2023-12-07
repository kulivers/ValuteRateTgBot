using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class GetByVchInfoHandler : IActionHandler
{
    public string ActionName => ActionNames.ValuteRateSettings.GetByVchInfo;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Valute.GetRateVch);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "input vch code to get valute rate",
            cancellationToken: token);
    }
}

public class GetByVchHandler : IActionHandler
{
    private readonly IValuteRateProvider _valuteRateProvider;

    public GetByVchHandler(IValuteRateProvider valuteRateProvider)
    {
        _valuteRateProvider = valuteRateProvider;
    }
    public string ActionName => ActionNames.ValuteRateSettings.GetByVch;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Default);
        if (_valuteRateProvider.TryGetCurrentRate(message, out var result))
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: result.Vcurs.ToString(),
                cancellationToken: token);
        }
        else
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "invalid vch code",
                cancellationToken: token);
        }
    }
}
