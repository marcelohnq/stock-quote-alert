using MediatR;
using Microsoft.Extensions.Logging;
using StockQuote.Core.Interfaces;
using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UseCases.Quotes.Alert;

public class AlertQuoteHandler(IRepository<Quote> _repository, ILogger<AlertQuoteHandler> _logger) : IRequestHandler<AlertQuoteCommand, bool>
{
    public async Task<bool> Handle(AlertQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (quote is null)
        {
            _logger.LogError("O ativo com ID [{Id}] não foi encontrado.", request.Id);
            return false;
        }

        quote.AddPrice(new(new(request.Price, request.DatePrice)));

        await _repository.UpdateAsync(quote, cancellationToken);

        return true;
    }
}
