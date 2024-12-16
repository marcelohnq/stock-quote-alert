using Microsoft.EntityFrameworkCore;
using StockQuote.Core.BaseClasses;
using StockQuote.Core.Interfaces;

namespace StockQuote.Infrastructure.Data;

public class EfRepository<T>(QuoteContext _dbContext) : IRepository<T> where T : EntityBase, IAggregateRoot
{
    private readonly DbSet<T> _dbSet = _dbContext.Set<T>();

    public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull =>
        await _dbSet.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken) =>
        await _dbSet.ToListAsync(cancellationToken);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
