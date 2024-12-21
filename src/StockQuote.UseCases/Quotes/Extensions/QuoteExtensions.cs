using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UseCases.Quotes.Extensions;

public static class QuoteExtensions
{
    public static QuoteDto ParserDTO(this Quote quote)
        => new(quote.Id, quote.Asset.Ticker, quote.LimitAlert.Up, quote.LimitAlert.Down, null);
}
