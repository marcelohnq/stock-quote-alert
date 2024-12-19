using StockQuote.Core.Interfaces;

namespace StockQuote.Infrastructure.API;

public class APIQuoteService : IAPIQuote
{
    public Task<decimal> GetCurrentQuote(string ticker)
    {
        throw new NotImplementedException();
    }
}
