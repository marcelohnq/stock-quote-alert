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

    [Fact]
    public void AddPrice_QuotePriceUp_Success()
    {
        var quote = new Quote(new(DefaultTicker), new(DefaultUp, DefaultDown));
        var quotePrice = new QuotePrice(new(DefaultUp + 0.33M, DateTime.Now));

        quote.AddPrice(quotePrice);

        Assert.Equal(quotePrice.Price.Value, quote.Prices.First().Price.Value);
        Assert.Equal(quotePrice.Price.Date, quote.Prices.First().Price.Date);
    }

    [Fact]
    public void AddPrice_QuotePriceNull_ThrowsArgumentNullException()
    {
        var quote = new Quote(new(DefaultTicker), new(DefaultUp, DefaultDown));

        var exception = Assert.Throws<ArgumentNullException>(() => quote.AddPrice(null!));
        Assert.Equal("O preço precisa ser informado. (Parameter 'price')", exception.Message);
    }
}
