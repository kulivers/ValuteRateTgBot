using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Services;

namespace Telegram.PriceCalculator.Router.Menu.Formula;

public class DeleteFormulaHandler : ActionHandler
{
    private IFormulaCalculationManager _manager;

    public DeleteFormulaHandler(IFormulaCalculationManager manager)
    {
        _manager = manager;
    }
    public override string ActionName => ActionNames.FormulaSettings.DeleteFormula;
    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        var formula = _manager.GetByUserId(userId);
        if (formula == default)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You havent got formula",
                cancellationToken: token);
        }

        await _manager.Delete(formula);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Done.",
            cancellationToken: token);

    }

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
