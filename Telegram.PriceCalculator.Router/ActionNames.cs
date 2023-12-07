namespace Telegram.PriceCalculator.Router;

public static class ActionNames
{
    public const string Default = "";

    public static class ValuteRateSettings
    {
        public const string UpdateRates = "update Valute";
        public const string GetByVch = "get curse by vch";
        public const string GetAllVch = "get all vch info";
    }

    public static class FormulaSettings
    {
        public const string SetupNewFormula = "SetupNewFormula";
        public const string GetFormula = "GetFormula";
        public const string ListFormulas = "ListFormulas";
        public const string EditFormula = "EditFormula";
    }

    public static class Menu
    {
        public const string ValuteRateSettings = "Valute rate settings";
        public const string FormulaSettings = "FormulaSettings";
    }
}
