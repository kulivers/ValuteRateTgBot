using Telegram.Bot;
using Telegram.PriceCalculator.Services;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router.Menu.Formula;


public class SetupNewFormulaHandler : IActionHandler
{
    public string ActionName => ActionNames.FormulaSettings.SetupNewFormulaInfo;

    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
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
}

public class SetupNewFormulaInputHandler : IActionHandler
{
    private IFormulaCalculationManager _calculationManager;

    public SetupNewFormulaInputHandler(IFormulaCalculationManager formulaCalculationManager)
    {
        _calculationManager = formulaCalculationManager;
    }
    public string ActionName => ActionNames.FormulaSettings.SetupNewFormulaInput;

    public async Task Handle(ITelegramBotClient botClient, UserContext userContext, string message, long userId, long chatId, CancellationToken token)
    {
        userContext.Set(userId, Routes.Default);
        await _calculationManager.Create(message, userId);
    }
}
