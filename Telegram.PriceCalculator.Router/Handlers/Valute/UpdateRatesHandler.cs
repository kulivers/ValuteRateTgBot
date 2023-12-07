using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class UpdateRatesHandler : ActionHandler
{
    private readonly IValuteRateProvider _valuteRateProvider;

    public UpdateRatesHandler(IValuteRateProvider valuteRateProvider)
    {
        _valuteRateProvider = valuteRateProvider;
    }

    public override string ActionName => ActionNames.ValuteRateSettings.UpdateRates;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update)
    {
        userContext.Set(update.Message.From.Id, Routes.Valute.ForceUpdate);
        var result = false;
        try
        {
            await _valuteRateProvider.UpdateRate();
        }
        catch
        {

        }
        await botClient.SendTextMessageAsync(
            chatId: update.Message.Chat.Id,
            text: result ? "Updated successfully" : "Failed to process request",
            cancellationToken: CancellationToken.None);

    }
}
