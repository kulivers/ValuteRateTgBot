using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class UpdateRatesHandler : IActionHandler
{
    private readonly IValuteRateProvider _valuteRateProvider;

    public UpdateRatesHandler(IValuteRateProvider valuteRateProvider)
    {
        _valuteRateProvider = valuteRateProvider;
    }

    public string ActionName => ActionNames.ValuteRateSettings.UpdateRates;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Valute.ForceUpdate);
        var result = false;
        try
        {
            await _valuteRateProvider.UpdateRate();
            result = true;
        }
        catch
        {

        }
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: result ? "Updated successfully" : "Failed to process request to central bank",
            cancellationToken: CancellationToken.None);
    }
}
