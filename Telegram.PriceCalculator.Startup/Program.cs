using CentralBankSDK;
using Telegram.Bot;
using Telegram.Bot.Examples.Polling;
using Telegram.Bot.Examples.Polling.Services;
using Telegram.PriceCalculator.Calculator;
using Telegram.PriceCalculator.Calculator.Api;
using Telegram.PriceCalculator.Repository;
using Telegram.PriceCalculator.Router;
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
                   services.AddScoped<UserContextStorage>();
                   services.AddScoped<RoutesStorageTree>();

                   //CalculationServices
                   services.AddScoped<IHardcodedCalculationService, HardCodedService>();
                   services.AddScoped<IFormulaCalculationService, FormulaCalculationService>();

                   //repositories
                   services.AddScoped<RepositoryBase<UserFormulaDto, string>, UserFormulasRepository>();


                   services.AddScoped<ICentralBankService, CentralBankService>();
                   services.AddHostedService<PollingService>();
                   services.AddHostedService<ValuteRateProvider>();
               })
               .Build();

await host.RunAsync();
