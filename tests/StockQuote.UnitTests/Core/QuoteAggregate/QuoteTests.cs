using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class QuoteTests
{
    private const string DefaultTicker = "PETR4";
    private const decimal DefaultUp = 38.15M;
    private const decimal DefaultDown = 38.07M;

    [Fact]
    public void Constructor_Quote_SuccessCreate()
    {
        var ticker = DefaultTicker;
        var quoteUp = DefaultUp;
        var quoteDown = DefaultDown;

        var quote = new Quote(new(ticker), new(quoteUp, quoteDown));

        Assert.Multiple(
            () => Assert.Equal(ticker, quote.Asset.Ticker),
            () => Assert.Equal(quoteUp, quote.LimitAlert.Up),
            () => Assert.Equal(quoteDown, quote.LimitAlert.Down),
            () => Assert.NotNull(quote.Audit),
            () => Assert.Empty(quote.Prices));
    }

    [Fact]
    public void Constructor_QuoteAssetNull_ThrowsArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Quote(null!, new(DefaultUp, DefaultDown)));
        Assert.Equal("É necessário informar um ativo. (Parameter 'asset')", exception.Message);
    }

    [Fact]
    public void Constructor_QuoteLimitNull_ThrowsArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Quote(new(DefaultTicker), null!));
        Assert.Equal("É necessário informar o limite de alerta para a cotação. (Parameter 'limit')", exception.Message);
    }

    [Theory]
    [InlineData(38.33, 38.22, 38.21)]
    [InlineData(38.33, 38.22, 38.34)]
    [InlineData(38.33, 38.22, 38.15)]
    [InlineData(38.33, 38.22, 38.41)]
    public void AddPrice_QuotePrice_Success(decimal up, decimal down, decimal current)
    {
        var quote = new Quote(new(DefaultTicker), new(up, down));
        var quotePrice = new QuotePrice(new(current, DateTime.Now));

        quote.AddPrice(quotePrice);

        Assert.Equal(quotePrice.Price.Value, quote.Prices.First().Price.Value);
        Assert.Equal(quotePrice.Price.Date, quote.Prices.First().Price.Date);
        Assert.Single(quote.DomainEvents);
    }

    [Fact]
    public void AddPrice_QuotePriceNull_ThrowsArgumentNullException()
    {
        var quote = new Quote(new(DefaultTicker), new(DefaultUp, DefaultDown));

        var exception = Assert.Throws<ArgumentNullException>(() => quote.AddPrice(null!));
        Assert.Equal("O preço precisa ser informado. (Parameter 'quotePrice')", exception.Message);
    }

    [Theory]
    [InlineData(38.33, 38.22, 38.22)]
    [InlineData(38.33, 38.22, 38.33)]
    [InlineData(38.33, 38.22, 38.28)]
    public void AddPrice_QuotePriceInvalid_ThrowsArgumentException(decimal up, decimal down, decimal current)
    {
        var quote = new Quote(new(DefaultTicker), new(up, down));
        var price = new Price(current, DateTime.Now);

        var exception = Assert.Throws<ArgumentException>(() => quote.AddPrice(new(price)));
        Assert.Equal("O preço precisa ser maior ou menor que os limites definidos. (Parameter 'quotePrice')", exception.Message);
    }

    [Fact]
    public void AlterLimitAlert_Quote_Success()
    {
        var alterUp = 99.99M;
        var alterDown = 0.99M;
        var quote = new Quote(new(DefaultTicker), new(DefaultUp, DefaultDown));

        quote.AlterLimitAlert(new(alterUp, alterDown));

        Assert.Equal(alterUp, quote.LimitAlert.Up);
        Assert.Equal(alterDown, quote.LimitAlert.Down);
    }
}
