using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockQuote.Core.Interfaces;
using StockQuote.Infrastructure.Data;
using StockQuote.Infrastructure.Email;

namespace StockQuote.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
      this IServiceCollection services,
      ConfigurationManager config,
      ILogger logger)
    {
        string? connectionString = config.GetConnectionString("DefaultConnection");
        Guard.Against.Null(connectionString);

        services.AddDbContext<QuoteContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
               .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddScoped<IEmailSender, FakeEmailSender>();

        logger.LogInformation("Infrastructure - serviços registrados com sucesso");

        return services;
    }
}