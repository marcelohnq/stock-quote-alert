using StockQuote.Core.Interfaces;

namespace StockQuote.Infrastructure.API;

public class ApiQuoteService : IApiQuote
{
    public Task<decimal> GetCurrentQuote(string ticker)
    {
        throw new NotImplementedException();
    }
}
