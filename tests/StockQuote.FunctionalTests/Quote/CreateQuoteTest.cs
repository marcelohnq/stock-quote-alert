using Microsoft.EntityFrameworkCore;
using StockQuote.UseCases.Quotes.Create;

namespace StockQuote.FunctionalTests.Quote;

public class CreateQuoteTest(ApplicationFixture _app) : IClassFixture<ApplicationFixture>
{
    [Fact]
    public async Task Command_CreateQuote()
    {
        var command = new CreateQuoteCommand("PETR4", 38.22M, 38.11M);
        var result = await _app.Mediator.Send(command);

        Assert.NotNull(result);

        var get = await _app.DbContext.Quotes.FirstAsync(q => q.Id == result.Id);

        Assert.Multiple(
            () => Assert.Equal(result.Id, get.Id),
            () => Assert.Equal(command.Ticker, get.Asset.Ticker),
            () => Assert.Equal(result.LimitUp, get.LimitAlert.Up),
            () => Assert.Equal(result.LimitDown, get.LimitAlert.Down));
    }

    [Fact]
    public async Task Command_CreateQuote_Duplicate()
    {
        var command = new CreateQuoteCommand("PETR4", 38.22M, 38.11M);
        var quote = await _app.Mediator.Send(command);
        var sameQuote = await _app.Mediator.Send(command);

        Assert.NotNull(quote);
        Assert.NotNull(sameQuote);
        Assert.Equal(quote.Id, sameQuote.Id);

        var gets = await _app.DbContext.Quotes.ToListAsync();

        Assert.Single(gets);
    }
}
