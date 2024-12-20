using MediatR;
using StockQuote.Core.Interfaces;
using StockQuote.Core.QuoteAggregate;
using StockQuote.UseCases.Quotes.Extensions;

namespace StockQuote.UseCases.Quotes.List;

public class ListQuotesHandler(IReadRepository<Quote> _repository) : IRequestHandler<ListQuotesQuery, IEnumerable<QuoteDto>>
{
    public async Task<IEnumerable<QuoteDto>> Handle(ListQuotesQuery _, CancellationToken cancellationToken)
    {
        var listQuotes = await _repository.ListAsync(cancellationToken);

        return listQuotes.OrderByDescending(q => q.Id).Select(q => q.ParserDTO());
    }
}
