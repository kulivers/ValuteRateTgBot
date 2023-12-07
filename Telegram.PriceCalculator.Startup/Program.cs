using CentralBankSDK;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Examples.Polling;
using Telegram.Bot.Examples.Polling.Services;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Contracts;
using Telegram.PriceCalculator.Repository;
using Telegram.PriceCalculator.Router;
using Telegram.PriceCalculator.Router.Handlers.Menus;
using Telegram.PriceCalculator.Router.Menu;
using Telegram.PriceCalculator.Router.Menu.Valute;
using Telegram.PriceCalculator.Services;
using Telegram.PriceCalculator.Shared;

var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices((context, services) =>
               {
                   // Register Bot configuration
                   services.Configure<BotConfiguration>(
                       context.Configuration.GetSection(BotConfiguration.Configuration));

                   // Register named HttpClient to benefits from IHttpClientFactory
                   // and consume it with ITelegramBotClient typed client.
                   // More read:
                   //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
                   //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
                   services.AddHttpClient("telegram_bot_client")
                           .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                           {
                               var botConfig = sp.GetConfiguration<BotConfiguration>();
                               TelegramBotClientOptions options = new(botConfig.BotToken);
                               return new TelegramBotClient(options, httpClient);
                           });

                   services.AddScoped<UpdateHandler>();
                   services.AddScoped<ReceiverService>();

                   //ROUTER
                   services.AddScoped<MessageRouter>();
                   services.AddScoped<UserContext>();
                   services.AddScoped<RoutesStorageTree>();

                   //actionHandlers
                   services.AddScoped<ActionHandler, DefaultActionHandler>();
                   services.AddScoped<ActionHandler, ValuteRateSettingsMenu>();
                   services.AddScoped<ActionHandler, FormulaSettingsSettingsMenu>();
                   services.AddScoped<ActionHandler, UpdateRatesHandler>();
                   services.AddScoped<ActionHandler, GetAllVchHandler>();

                   //repositories
                   services.AddScoped<IRepositoryManager, RepositoryManager>();
                   services.AddDbContext<RepositoryContext>(builder =>
                   {
                       builder.EnableSensitiveDataLogging();
                       builder.EnableDetailedErrors();
                       builder.UseMySQL("Server=127.0.0.1;Uid=ekul;Pwd=ekul;Database=valutebot",
                           optionsBuilder => optionsBuilder.MigrationsAssembly("Telegram.PriceCalculator.Repository"));
                   });

                   services.AddScoped<ICentralBankService, CentralBankService>();
                   services.AddHostedService<PollingService>();
                   services.AddHostedApiService<IValuteRateProvider, ValuteRateProvider>();
               })
               .Build();

await host.RunAsync();
