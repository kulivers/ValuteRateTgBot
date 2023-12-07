using System.Reflection.Metadata.Ecma335;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.PriceCalculator.Router.Menu;

public abstract class ActionHandler
{
    public static string ActionName { get; }
    public abstract Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update);
}
