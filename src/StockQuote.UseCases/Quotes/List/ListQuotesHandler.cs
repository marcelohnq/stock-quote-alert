using MediatR;
using StockQuote.UseCases.Interfaces;

namespace StockQuote.UseCases.Quotes.List;

public class ListQuotesHandler(IQueryListQuote _query) : IRequestHandler<ListQuotesQuery, IEnumerable<QuoteDto>>
{
    public async Task<IEnumerable<QuoteDto>> Handle(ListQuotesQuery _, CancellationToken cancellationToken)
    {
        var listQuotes = await _query.ListQuoteAsync(cancellationToken);

        return listQuotes?.OrderByDescending(q => q.Id).ToList() ?? [];
    }
}
