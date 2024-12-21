using StockQuote.UseCases.Quotes;

namespace StockQuote.UseCases.Interfaces;

public interface IQueryListQuote
{
    Task<IEnumerable<QuoteDto>?> ListQuoteAsync(CancellationToken cancellationToken = default);
}
