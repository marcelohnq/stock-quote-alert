using Microsoft.EntityFrameworkCore;
using StockQuote.UseCases.Quotes.Alert;
using StockQuote.UseCases.Quotes.Create;

namespace StockQuote.FunctionalTests.Quote;

public class AlertQuoteTest(ApplicationFixture _app) : IClassFixture<ApplicationFixture>
{
    [Fact]
    public async Task Command_AlertQuote()
    {
        var command = new CreateQuoteCommand("PETR4", 38.22M, 38.11M);
        var quote = await _app.Mediator.Send(command);
        Assert.NotNull(quote);

        var command2 = new AlertQuoteCommand(quote.Id, 38.44M, DateTime.Now);
        var result = await _app.Mediator.Send(command2);
        Assert.True(result);

        var get = await _app.DbContext.Quotes.FirstAsync(q => q.Id == quote.Id);

        Assert.Multiple(
            () => Assert.Equal(quote.Id, get.Id),
            () => Assert.Equal(command2.Price, get.Prices.First().Price.Value),
            () => Assert.Equal(command2.DatePrice, get.Prices.First().Price.Date));
    }

    [Fact]
    public async Task Command_QuoteNotExists()
    {
        var command = new AlertQuoteCommand(99, 38.44M, DateTime.Now);
        var result = await _app.Mediator.Send(command);
        Assert.False(result);
    }
}