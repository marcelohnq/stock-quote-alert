using Microsoft.EntityFrameworkCore;
using StockQuote.Core.QuoteAggregate;

namespace StockQuote.IntegrationTests.Data;

public class QuoteRepositoryTests(IntegrationTestBase _base) : IClassFixture<IntegrationTestBase>
{
    [Fact]
    public async Task RespositoryAdd_Quote_Success()
    {
        var tickerName = "PETR4";
        var limitUp = 38.22M;
        var limitDown = 38.14M;
        var quote = new Quote(new(tickerName), new(limitUp, limitDown));

        await _base.QuoteRepository.AddAsync(quote);

        var result = (await _base.QuoteRepository.ListAsync()).First();

        Assert.NotNull(result);
        Assert.Multiple(
            () => Assert.Equal(tickerName, result.Asset.Ticker),
            () => Assert.Equal(limitUp, result.LimitAlert.Up),
            () => Assert.Equal(limitDown, result.LimitAlert.Down),
            () => Assert.NotNull(quote.Audit),
            () => Assert.Empty(quote.Prices));
    }

    [Fact]
    public async Task RespositoryUpdate_Quote_Success()
    {
        var tickerName = "PETR4";
        var limitUp = 38.22M;
        var limitDown = 38.14M;
        var quote = new Quote(new(tickerName), new(limitUp, limitDown));

        var newQuote = await _base.QuoteRepository.AddAsync(quote);

        Assert.NotNull(newQuote);

        var price = new Price(39.22M, DateTime.Now);
        newQuote.AddPrice(new(price));
        await _base.QuoteRepository.UpdateAsync(newQuote);

        _base.DbContext.Entry(newQuote).State = EntityState.Detached;

        var result = await _base.QuoteRepository.GetByIdAsync(newQuote.Id);

        Assert.NotNull(result);
        Assert.NotSame(newQuote, result);
        Assert.Multiple(
            () => Assert.Equal(price.Value, quote.Prices.First().Price.Value),
            () => Assert.Equal(price.Date, quote.Prices.First().Price.Date));
    }
}
