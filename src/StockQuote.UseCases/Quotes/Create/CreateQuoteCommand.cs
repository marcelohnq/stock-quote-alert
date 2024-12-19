using MediatR;

namespace StockQuote.UseCases.Quotes.Create;

public record CreateQuoteCommand(string Ticker, decimal LimitUp, decimal LimitDown) : IRequest<QuoteDto?>;