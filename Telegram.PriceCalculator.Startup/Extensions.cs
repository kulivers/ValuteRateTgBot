using Microsoft.Extensions.Options;

namespace Telegram.Bot.Examples.Polling;

public static class DIExcensions // =)
{
    /// <summary>
    /// Used to register <see cref="IHostedService"/> class which defines an referenced <typeparamref name="TInterface"/> interface.
    /// </summary>
    /// <typeparam name="TInterface">The interface other components will use</typeparam>
    /// <typeparam name="TService">The actual <see cref="IHostedService"/> service.</typeparam>
    /// <param name="services"></param>
    public static void AddHostedApiService<TInterface, TService>(this IServiceCollection services)
        where TInterface : class
        where TService : class, IHostedService, TInterface
    {
        services.AddSingleton<TInterface, TService>();
        services.AddSingleton<IHostedService>(p => (TService) p.GetService<TInterface>());
    }
}
public static class PollingExtensions
{
    public static T GetConfiguration<T>(this IServiceProvider serviceProvider)
        where T : class
    {
    var o = serviceProvider.GetService<IOptions<T>>();
    if (o is null)
        throw new ArgumentNullException(nameof(T));

    return o.Value;
    }
}
