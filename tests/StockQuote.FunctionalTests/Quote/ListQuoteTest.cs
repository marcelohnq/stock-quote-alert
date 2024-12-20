using StockQuote.UseCases.Quotes.Create;
using StockQuote.UseCases.Quotes.List;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StockQuote.FunctionalTests.Quote;

public class ListQuoteTest(ApplicationFixture _app) : IClassFixture<ApplicationFixture>
{
    [Fact]
    public async Task Query_ListQuote()
    {
        var result1 = await _app.Mediator.Send(new CreateQuoteCommand("PETR4", 38.22M, 38.11M));
        var result2 = await _app.Mediator.Send(new CreateQuoteCommand("ITUB4", 31.12M, 31.07M));
        var result3 = await _app.Mediator.Send(new CreateQuoteCommand("BBAS3", 23.86M, 23.80M));

        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);

        var list = (await _app.Mediator.Send(new ListQuotesQuery())).ToList();

        Assert.Multiple(
            () => Assert.Equal(result3.Id, list[0].Id),
            () => Assert.Equal(result3.Ticker, list[0].Ticker),
            () => Assert.Equal(result3.LimitUp, list[0].LimitUp),
            () => Assert.Equal(result3.LimitDown, list[0].LimitDown));

        Assert.Multiple(
            () => Assert.Equal(result2.Id, list[1].Id),
            () => Assert.Equal(result2.Ticker, list[1].Ticker),
            () => Assert.Equal(result2.LimitUp, list[1].LimitUp),
            () => Assert.Equal(result2.LimitDown, list[1].LimitDown));

        Assert.Multiple(
            () => Assert.Equal(result1.Id, list[2].Id),
            () => Assert.Equal(result1.Ticker, list[2].Ticker),
            () => Assert.Equal(result1.LimitUp, list[2].LimitUp),
            () => Assert.Equal(result1.LimitDown, list[2].LimitDown));
    }
}
