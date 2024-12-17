using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuote.Console.Configuration;
using StockQuote.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

var logger = builder.Logging.Services.BuildServiceProvider()
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger<Program>();

logger.LogInformation("Start - StockQuote sendo iniciado!");

builder.Services.AddMediatrConfigs();
builder.Services.AddInfrastructureServices(builder.Configuration, logger);

using IHost host = builder.Build();

await host.RunAsync();