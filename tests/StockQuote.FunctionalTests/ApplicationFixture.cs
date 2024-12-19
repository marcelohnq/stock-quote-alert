using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuote.Infrastructure.Data;
using Testcontainers.MsSql;

namespace StockQuote.FunctionalTests;

public class ApplicationFixture : IDisposable
{
    private readonly MsSqlContainer _databaseContainer = new MsSqlBuilder()
        .WithCleanUp(true)
        .WithName($"Quote-{Guid.NewGuid()}")
        .Build();

    public IHost Host { get; private set; }

    public ApplicationFixture()
    {
        var builder = Program.CreateApplicationBuilder([], out ILogger _);
        ConfigureHost(builder.Services);
        builder.Environment.EnvironmentName = "Development";
        Host = builder.Build();

        using var scope = Host.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<QuoteContext>();

        // Ensure the database is created.
        db.Database.Migrate();
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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Host.Dispose();
        }
    }
}
