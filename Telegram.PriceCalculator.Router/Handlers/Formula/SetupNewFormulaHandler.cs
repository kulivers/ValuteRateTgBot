using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.PriceCalculator.Services;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Formula;


public class SetupNewFormulaHandler : ActionHandler
{
    public override string ActionName => ActionNames.FormulaSettings.SetupNewFormulaInfo;

    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Formula.Formulacreate);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Text formula next msg. \n " +
                  "You can use variables like x or y, and define them later. \n " +
                  "special variables are vch codes and USER, write them in upper case. \n" +
                  "USER = input value that will be written here\n" +
                  "VCH&& code is current curse of vch valute" +
                  "Example: x*2 + USD + x + USER. \n" +
                  "Later set variable x = 1337. and USD will be taken from central bank api, and USER is the digit that you will input. \n\n" +
                  "So, write your formula and variables in new line: \n" +
                  "Example: \n" +
                  "x + y + USER + USD\n" +
                  "x 1\n" +
                  "y 5",
            cancellationToken: token);
    }

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}

public class SetupNewFormulaInputHandler : ActionHandler
{
    private IFormulaCalculationManager _calculationManager;

    public SetupNewFormulaInputHandler(IFormulaCalculationManager formulaCalculationManager)
    {
        _calculationManager = formulaCalculationManager;
    }
    public override string ActionName => ActionNames.FormulaSettings.SetupNewFormulaInput;

    public override async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Default);
        var result = await _calculationManager.Create(message, userId);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: result ? "Done." : "Formula has errors. It hasnt created",
            cancellationToken: token);
    }

    public override Task Handle(ITelegramBotClient botClient, UserContext userContext, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
