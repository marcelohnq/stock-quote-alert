namespace StockQuote.Core.Interfaces;

public interface IQuoteHttpService
{
    Task<string?> SearchTicker(string assetTicker);
}
