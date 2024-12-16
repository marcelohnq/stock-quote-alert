using Microsoft.Extensions.DependencyInjection;
using StockQuote.Core.QuoteAggregate;
using System.Reflection;

namespace StockQuote.Console.Configuration;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Quote)), // Core
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));

        return services;
    }
}
