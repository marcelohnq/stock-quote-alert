using MediatR;

namespace StockQuote.UseCases.Quotes.Alert;

public record AlertQuoteCommand(int Id, decimal Price) : IRequest<bool>;