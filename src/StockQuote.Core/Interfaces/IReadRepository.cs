namespace StockQuote.Core.Interfaces;

public interface IReadRepository<T> where T : class, IAggregateRoot
{
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken) where TId : notnull;
    Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken);
}
