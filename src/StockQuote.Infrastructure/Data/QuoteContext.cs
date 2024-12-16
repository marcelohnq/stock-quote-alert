using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StockQuote.Core.BaseClasses;
using StockQuote.Core.Interfaces;
using StockQuote.Core.QuoteAggregate;
using System.Reflection;

namespace StockQuote.Infrastructure.Data;

public class QuoteContext(IMediator _mediator, DbContextOptions<QuoteContext> options) : DbContext(options)
{
    public DbSet<Quote> Quotes => Set<Quote>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<HasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .ToArray();

        await PublishDomainEventsAsync(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges() =>
          SaveChangesAsync().GetAwaiter().GetResult();

    private async Task PublishDomainEventsAsync(IEnumerable<HasDomainEvents> entitiesWithEvents)
    {
        foreach (var hasDomainEvents in entitiesWithEvents)
        {
            IDomainEvent[] events = [.. hasDomainEvents.DomainEvents];
            hasDomainEvents.ClearDomainEvents();

            foreach (IDomainEvent domainEvent in events)
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}