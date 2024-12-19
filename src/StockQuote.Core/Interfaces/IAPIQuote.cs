namespace StockQuote.Core.Interfaces;

public interface IAPIQuote
{
    Task<decimal> GetCurrentQuote(string ticker);
}
