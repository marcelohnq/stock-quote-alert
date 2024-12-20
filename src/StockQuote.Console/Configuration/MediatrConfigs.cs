using Microsoft.Extensions.DependencyInjection;
using StockQuote.Core.QuoteAggregate;
using StockQuote.UseCases.Quotes.List;
using System.Reflection;

namespace StockQuote.Console.Configuration;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Quote)), // Core
            Assembly.GetAssembly(typeof(ListQuotesQuery)), // UseCases
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));

        return services;
    }
}
