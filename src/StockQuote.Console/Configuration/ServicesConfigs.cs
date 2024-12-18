using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockQuote.Console.Command;
using StockQuote.Console.Service;
using StockQuote.Core.Interfaces;
using StockQuote.Infrastructure.Email;

namespace StockQuote.Console.Configuration;

public static class ServicesConfigs
{
    public static IServiceCollection AddServicesConfigs(this IServiceCollection services, IHostApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            services.AddScoped<IEmailSender, FakeEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, FakeEmailSender>();
        }

        services.AddScoped<CommandRequest>();
        services.AddHostedService<QuoteService>();

        return services;
    }
}
