using System.Reflection.Metadata.Ecma335;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.PriceCalculator.Router.Menu;

public abstract class ActionHandler//todo this is interface
{
    public abstract string ActionName { get; }
    public abstract Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token);
}
