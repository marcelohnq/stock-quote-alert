namespace StockQuote.Core.Interfaces;

public interface IApiQuote
{
    Task<decimal?> GetCurrentQuote(string ticker);
}
