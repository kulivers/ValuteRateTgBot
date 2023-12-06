using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.PriceCalculator.Presentation;

public class BaseComponent
{
    ReplyKeyboardMarkup replyKeyboardMarkup = new(
        new[]
        {
            new KeyboardButton[] { "1.1", "1.2" },
            new KeyboardButton[] { "2.1", "2.2" },
        })
    {
        ResizeKeyboard = true
    };
}

public static class TgViewsFactory
{
    public static IReplyMarkup GetMenu(params string[] actions) =>
        new ReplyKeyboardMarkup(
            new[]
            {
                actions.Select(action => new KeyboardButton(action))
            })
        {
            ResizeKeyboard = true,
        };
}

public interface ITgView
{
    IReplyMarkup? Reply { get; set; }
    string? Text { get; set; }
}
