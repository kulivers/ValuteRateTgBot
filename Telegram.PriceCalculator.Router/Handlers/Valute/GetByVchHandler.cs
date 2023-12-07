using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class GetByVchHandler : IActionHandler
{
    public string ActionName => ActionNames.ValuteRateSettings.GetByVch;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Valute.GetRateVch);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "inpute some",
            cancellationToken: token);
        Здесь все, в дефолтном ждем входа
    }
}
