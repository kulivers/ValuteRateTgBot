namespace Telegram.PriceCalculator.Shared;

public static class Routes
{
    public static class Valute
    {
        public const string Root = "/valute";
        public const string ForceUpdate = "/valute/force_update_valute";
        public const string GetAllRates = "/valute/get_all_rates";
        public const string GetRateVch = "/valute/get_rate_vch";
        public const string GetRateCountry = "/valute/get_rate_country";
    }

    public static class Formula
    {
        public const string Root = "/formula";
        public const string FormulaList = "/formula/list";
        public const string Formulacreate = "/formula/create";
        public const string Formulaedit = "/formula/edit";
        public const string Formuladelete = "/formula/delete";

        public static class Vars
        {
            public const string Root = "/formula/vars";
            public const string List = "/formula/vars/list";
            public const string Create = "/formula/vars/create";
            public const string Edit = "/formula/vars/edit";
            public const string Delete = "/formula/vars/delete";
        }
    }

    public const string Menu = "/Menu";
    public const string Default = "";
}
