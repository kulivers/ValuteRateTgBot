using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Presentation;
using Telegram.PriceCalculator.Services;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu;

public class DefaultActionHandler : IActionHandler
{
    private IFormulaCalculationManager _calcManager;

    public DefaultActionHandler(IFormulaCalculationManager calculationManager)
    {
        _calcManager = calculationManager;
    }
    public string ActionName => ActionNames.Default;
    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            await ShowMenu(botClient, chatId, token);
            return;
        }

        if (decimal.TryParse(message, out var result))
        {
            var formula = _calcManager.GetByUserId(userId);
            if (formula==null)
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You havent formulas",
                    cancellationToken: token);
                return;
            }

            formula.Variables?.Add(new Variable(){Name = "USER", Value = result});
            if (_calcManager.TryCalculateResult(formula, out result))
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: result.ToString(),
                    cancellationToken: token);
            }
            else
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "cant calculate result",
                    cancellationToken: token);
            }
            //calculate for user formula //todo here is actions by context
            return;
        }

        await ShowMenu(botClient, chatId, CancellationToken.None);
    }

    private async Task<Message> ShowMenu(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        return await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "No handler for this action.",
            replyMarkup: TgViewsFactory.GetKeyboard(ActionNames.Menu.ValuteRateSettings, ActionNames.Menu.FormulaSettings),
            cancellationToken: cancellationToken);
    }

}
