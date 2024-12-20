using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuote.Infrastructure.Data;
using Testcontainers.MsSql;

namespace StockQuote.FunctionalTests;

public class ApplicationFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _databaseContainer = new MsSqlBuilder()
        .WithCleanUp(true)
        .WithName($"Quote-{Guid.NewGuid()}")
        .Build();

    public required IHost Host { get; set; }
    public required IServiceScope Scope { get; set; }
    public required QuoteContext DbContext { get; set; }
    public required IMediator Mediator { get; set; }

    public async Task InitializeAsync()
    {
        await _databaseContainer.StartAsync();
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

        var builder = Program.CreateApplicationBuilder([], out ILogger _);
        ConfigureHost(builder.Services);
        Host = builder.Build();

        Scope = Host.Services.CreateScope();
        Mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();
        DbContext = Scope.ServiceProvider.GetRequiredService<QuoteContext>();

        await DbContext.Database.MigrateAsync();
    }

    private void ConfigureHost(IServiceCollection services)
    {
        // Remover o registro de ApplicationDbContext do aplicativo.
        var descriptor = services.SingleOrDefault(
        d => d.ServiceType ==
                typeof(DbContextOptions<QuoteContext>));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<QuoteContext>(options =>
            options.UseSqlServer(_databaseContainer.GetConnectionString()));
    }

    public async Task DisposeAsync()
    {
        Host.Dispose();
        Scope.Dispose();
        await DbContext.DisposeAsync();
        await _databaseContainer.DisposeAsync();
    }
}
