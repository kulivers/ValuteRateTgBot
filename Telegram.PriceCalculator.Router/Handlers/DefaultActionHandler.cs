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

        if (decimal.TryParse(message, out var userValue))
        {
            var formula = _calcManager.GetByUserId(userId);
            if (formula == null)
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You havent formulas",
                    cancellationToken: token);
                return;
            }


            await HandleFormulaCalculation(botClient, chatId, token, formula, userValue);
            return;
        }

        await ShowMenu(botClient, chatId, CancellationToken.None);
    }

    private async Task HandleFormulaCalculation(ITelegramBotClient botClient, long chatId, CancellationToken token, UserFormula formula, decimal userValue)
    {
        if (_calcManager.TryCalculateResult(formula, userValue, out var result))
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
    }

    private async Task ShowMenu(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "No handler for this action.",
            replyMarkup: TgViewsFactory.GetKeyboard(ActionNames.Menu.ValuteRateSettings, ActionNames.Menu.FormulaSettings),
            cancellationToken: cancellationToken);
    }
}
