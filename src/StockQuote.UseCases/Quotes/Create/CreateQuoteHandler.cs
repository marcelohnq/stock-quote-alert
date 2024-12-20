using MediatR;
using Microsoft.Extensions.Logging;
using StockQuote.Core.Interfaces;
using StockQuote.Core.QuoteAggregate;
using StockQuote.UseCases.Quotes.Extensions;

namespace StockQuote.UseCases.Quotes.Create;

public class CreateQuoteHandler(IRepository<Quote> _repository, ILogger<CreateQuoteHandler> _logger) : IRequestHandler<CreateQuoteCommand, QuoteDto?>
{
    public async Task<QuoteDto?> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var tickerExists = await _repository.FirstOrDefaultAsync(q => q.Asset.Ticker == request.Ticker, cancellationToken);

        if (tickerExists is not null)
        {
            _logger.LogTrace("Já existe um registro para o ativo [{Ativo}]", request.Ticker);
            tickerExists.AlterLimitAlert(new(request.LimitUp, request.LimitDown));

            await _repository.UpdateAsync(tickerExists, cancellationToken);

            return tickerExists.ParserDTO();
        }

        var quote = new Quote(new(request.Ticker), new(request.LimitUp, request.LimitDown));
        var createdItem = await _repository.AddAsync(quote, cancellationToken);

        return createdItem.ParserDTO();
    }
}