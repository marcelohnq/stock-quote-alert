using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using StockQuote.Core.QuoteAggregate;
using StockQuote.Infrastructure.Data;
using Testcontainers.MsSql;

namespace StockQuote.IntegrationTests;

public class IntegrationTestBase : IAsyncLifetime
{
    private readonly MsSqlContainer databaseContainer = new MsSqlBuilder()
        .WithCleanUp(true)
        .WithName($"Quote-{Guid.NewGuid()}")
        .Build();

    public async Task InitializeAsync()
    {
        await databaseContainer.StartAsync();
        await CreateDbContext();
    }

    public required QuoteContext DbContext { get; set; }
    public required EfRepository<Quote> QuoteRepository { get; set; }

    public async Task CreateDbContext()
    {
        var options = CreateNewContextOptions();
        var mediatorMock = new Mock<IMediator>();

        DbContext = new QuoteContext(options, mediatorMock.Object);
        QuoteRepository = new EfRepository<Quote>(DbContext);

        await DbContext.Database.MigrateAsync();
    }

    private DbContextOptions<QuoteContext> CreateNewContextOptions()
    {
        DbContextOptions<QuoteContext> options = new DbContextOptionsBuilder<QuoteContext>()
            .UseSqlServer(databaseContainer.GetConnectionString())
            .Options;

        return options;
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await databaseContainer.DisposeAsync();
    }
}
