using MediatR;

namespace StockQuote.UseCases.Quotes.List;

public record ListQuotesQuery() : IRequest<IEnumerable<QuoteDto>>;
