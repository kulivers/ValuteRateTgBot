using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.PriceCalculator.Presentation;

public static class TgViewsFactory //todo not static
{
    public static IReplyMarkup GetKeyboard(params string[] actions) =>
        new ReplyKeyboardMarkup(
            new[]
            {
                actions.Select(action => new KeyboardButton(action))
            })
        {
            ResizeKeyboard = true,
        };

    public static IReplyMarkup GetInlineKeyboard(IEnumerable<KeyValuePair<string, string>> actionsNCallbackData, byte rowCount, byte colCount)
    {
        var actions = actionsNCallbackData.ToArray();
        var buttons = new List<List<InlineKeyboardButton>>();
        for (var i = 0; i < rowCount; i++)
        {
            var row = new List<InlineKeyboardButton>();
            for (var j = 0; j < colCount; j++)
            {
                if (actions.Length< i+j)
                {
                    break;
                }

                var (action, callback) = actions[i+j];
                row.Add(callback == null ? new InlineKeyboardButton(action) : InlineKeyboardButton.WithCallbackData(action, callback));
            }
            buttons.Add(row);
        }


        InlineKeyboardMarkup inlineKeyboard = new(buttons);
        return inlineKeyboard;
    }

    public static IReplyMarkup GetValuteMenu(string[] actions)
    {
        return new ReplyKeyboardMarkup(actions.Select(action => new KeyboardButton(action)))
        {
            ResizeKeyboard = true
        };
    }
}

public interface ITgView
{
    IReplyMarkup? Reply { get; set; }
    string? Text { get; set; }
}
