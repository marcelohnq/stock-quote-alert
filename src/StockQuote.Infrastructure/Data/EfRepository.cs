using Microsoft.EntityFrameworkCore;
using StockQuote.Core.BaseClasses;
using StockQuote.Core.Interfaces;
using System.Linq.Expressions;

namespace StockQuote.Infrastructure.Data;

public class EfRepository<T>(QuoteContext _dbContext) : IRepository<T> where T : EntityBase, IAggregateRoot
{
    private readonly DbSet<T> _dbSet = _dbContext.Set<T>();

    public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull =>
        await _dbSet.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
