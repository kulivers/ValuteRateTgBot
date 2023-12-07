using System.Collections.Concurrent;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Router;

public class UserContext
{
    private readonly ConcurrentDictionary<long, string> _currentUsersRoutesContext;
    private readonly RoutesStorageTree _routesStorageTree;

    public UserContext(RoutesStorageTree tree)
    {
        _currentUsersRoutesContext = new ConcurrentDictionary<long, string>();
        _routesStorageTree = tree;
    }

    public string Get(long userId)
    {
        return _currentUsersRoutesContext.GetOrAdd(userId, RoutesStorageTree.DefaultPath);
    }

    public void Set(long userId, string route)
    {
        if (!_routesStorageTree.Contains(route))
        {
            throw new InvalidOperationException();
        }

        _currentUsersRoutesContext.AddOrUpdate(userId, route, (_, _) => route);
    }
}
