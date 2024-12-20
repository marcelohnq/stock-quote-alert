using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuote.Console.Command;
using StockQuote.Console.Configuration;
using StockQuote.Infrastructure;

var builder = CreateApplicationBuilder(args, out ILogger logger);
using IHost host = builder.Build();

if (args.Length < 1)
{
    logger.LogInformation("Nenhum argumento foi informada - Encerrando programa.");
    return;
}

using var scope = host.Services.CreateScope();
var command = scope.ServiceProvider.GetRequiredService<CommandRequest>();
await command.ExecuteCommand(args);

await host.RunAsync();

public static partial class Program
{
    public static HostApplicationBuilder CreateApplicationBuilder(string[] args, out ILogger logger)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

        logger = builder.Logging.Services.BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(Program));

        logger.LogInformation("Start - StockQuote sendo iniciado!");

        builder.Services.AddServicesConfigs(builder);
        builder.Services.AddMediatrConfigs();
        builder.Services.AddInfrastructureServices(builder.Configuration, logger);

        return builder;
    }
}