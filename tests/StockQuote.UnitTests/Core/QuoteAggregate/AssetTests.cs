using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class AssetTests
{
    private const string DefaultTicker = "PETR4";

    [Theory]
    [InlineData(DefaultTicker)]
    [InlineData("NIKE34")]
    [InlineData("B3SA3")]
    [InlineData("ITUB4")]
    [InlineData("SANB11")]
    [InlineData("itub4")]
    [InlineData("saNB11")]
    public void Constructor_Asset_SuccessCreate(string ticker)
    {
        var asset = new Asset(ticker);

        Assert.Equal(ticker.ToUpper(), asset.Ticker);
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
    public void Constructor_AssetTickerEmpty_ThrowsArgumentException(string ticker)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Asset(ticker));
        Assert.Equal("É necessário informar o ticker do ativo. (Parameter 'ticker')", exception.Message);
    }

    [Theory]
    [InlineData("P")]
    [InlineData("B3")]
    [InlineData("ITU")]
    [InlineData("PETR")]
    [InlineData("B3SA333")]
    [InlineData("petr")]
    [InlineData("itU")]
    public void Constructor_AssetTickerInvalid_ThrowsArgumentException(string ticker)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Asset(ticker));
        Assert.Equal("O ticker informado possui um formato inválido. (Parameter 'ticker')", exception.Message);
    }
}
