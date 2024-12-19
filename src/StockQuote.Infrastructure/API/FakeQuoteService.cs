using StockQuote.Core.Interfaces;

namespace StockQuote.Infrastructure.API;

public class FakeQuoteService : IApiQuote
{
    private readonly Random random = new();

    public async Task<decimal> GetCurrentQuote(string ticker)
    {
        await Task.Delay(300);

        return (decimal)(random.Next(1, 100) + random.NextDouble());
    }
}
