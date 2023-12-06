namespace Telegram.PriceCalculator.Shared;

public class RoutesStorageTree
{
    public const string DefaultPath = "";

    private readonly HashSet<string> _rootRoutes = new()
    {
        Routes.Valute.Root,
        Routes.Valute.ForceUpdate,
        Routes.Valute.GetAllRates,
        Routes.Valute.GetRateVch,
        Routes.Valute.GetRateCountry,

        Routes.Formula.Root,
        Routes.Formula.FormulaList,
        Routes.Formula.FormulaList,
        Routes.Formula.FormulaList,
        Routes.Formula.FormulaList,

        Routes.Formula.Vars.Root,
        Routes.Formula.Vars.List,
        Routes.Formula.Vars.Create,
        Routes.Formula.Vars.Edit,
        Routes.Formula.Vars.Delete,

        Routes.Default
    };

    public HashSet<string> GetRoutes() => new(_rootRoutes);

    public bool Contains(string route)
    {
        return _rootRoutes.Contains(route);
    }
}
