namespace Telegram.PriceCalculator.Router;

public class RoutesStorageTree
{
    public const string DefaultPath = "";
    private readonly HashSet<string> _rootRoutes = new()
    {
        "/valute",
        "/valute/force_update_valute",
        "/valute/get_all_rates",
        "/valute/get_rate_vch",
        "/valute/get_rate_country",

        "/formula",
        "/formula/list",
        "/formula/create",
        "/formula/edit",
        "/formula/delete",
        "/formula/vars",
        "/formula/vars/list",
        "/formula/vars/create",
        "/formula/vars/edit",
        "/formula/vars/delete",
        // "/inline_keyboard",
        // "/keyboard",
        // "/remove",
        // "/kiruha1337",
        // "/request",
        // "/inline_mode",
        // "/throw",
        // DefaultPath
    };

    public HashSet<string> GetRoutes() => new(_rootRoutes);

    public bool Contains(string route)
    {
        return _rootRoutes.Contains(route);
    }
}
