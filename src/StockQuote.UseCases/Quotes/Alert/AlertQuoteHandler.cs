using MediatR;
using StockQuote.Core.Interfaces;
using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UseCases.Quotes.Alert;

public class AlertQuoteHandler(IRepository<Quote> repository) : IRequestHandler<AlertQuoteCommand, bool>
{
    public Task<bool> Handle(AlertQuoteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
