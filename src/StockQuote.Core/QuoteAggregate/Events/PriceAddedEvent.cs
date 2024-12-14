using StockQuote.Core.Interfaces;

namespace StockQuote.Core.QuoteAggregate.Events;

internal sealed class PriceAddedEvent(string ticker, LimitAlert limit, Price price) : IDomainEvent
{
    public string AssetTicker { get; init; } = ticker;
    public decimal LimitUp { get; init; } = limit.Up;
    public decimal LimitDown { get; init; } = limit.Down;
    public decimal CurrentPrice { get; init; } = price.Value;
    public DateTime DatePrice { get; init; } = price.Date;
}