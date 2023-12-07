using Telegram.Bot;

namespace Telegram.PriceCalculator.Router.Menu.Valute;

public class GetByVchHandler : ActionHandler
{
    // ActionNames.ValuteRateSettings.GetByVch,

    public override string ActionName { get; }
    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
