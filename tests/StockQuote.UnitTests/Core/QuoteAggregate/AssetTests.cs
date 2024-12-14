namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class AssetTests
{
    private const string DefaultTicker = "PETR4";

    [Fact]
    public void Constructor_Asset_SuccessCreate()
    {
        var asset = new Asset(DefaultTicker);

        Assert.Equal(DefaultTicker, asset.Ticker);
    }

    [Fact]
    public void Constructor_AssetTickerNull_ThrowsArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Asset(null!));
        Assert.Equal("É necessário informar o ticker do ativo. (Parameter 'ticker')", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData("\r\n")]
    [InlineData("\f")]
    [InlineData("\u00A0")]
    [InlineData("\u2000")]
    [InlineData("\u3000")]
    public void Constructor_AssetTickerEmpty_ThrowsArgumentNullException(string ticker)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Asset(ticker));
        Assert.Equal("É necessário informar o ticker do ativo. (Parameter 'ticker')", exception.Message);
    }
}
