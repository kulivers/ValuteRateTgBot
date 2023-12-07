using System.Reflection.Metadata.Ecma335;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.PriceCalculator.Router.Menu;

public interface IActionHandler//todo this is interface
{
    public string ActionName { get; }
    public Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token);
}
