using MediatR;

namespace StockQuote.UseCases.Quotes.Alert;

public record AlertQuoteCommand(int Id, decimal Price, DateTime DatePrice) : IRequest<bool>;