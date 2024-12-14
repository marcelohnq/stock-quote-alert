using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class QuotePriceTests
{
    private const decimal DefaultPrice = 38.44M;

    [Fact]
    public void Constructor_QuotePrice_SuccessCreate()
    {
        var price = DefaultPrice;
        var date = DateTime.Now;

        var quotePrice = new QuotePrice(new(price, date));

        Assert.Multiple(
            () => Assert.Equal(price, quotePrice.Price.Value),
            () => Assert.Equal(date, quotePrice.Price.Date));
    }

    [Fact]
    public void Constructor_QuotePriceNull_ThrowsArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new QuotePrice(null!));
        Assert.Equal("O valor da cotação informado não pode ser nulo. (Parameter 'price')", exception.Message);
    }

    [Fact]
    public void AlterPrice_Price_Success()
    {
        var quotePrice = new QuotePrice(new(DefaultPrice, DateTime.Now.AddSeconds(-30)));
        var alterPrice = new Price(DefaultPrice + 0.5M, DateTime.Now);

        quotePrice.AlterPrice(alterPrice);

        Assert.Multiple(
            () => Assert.Equal(alterPrice.Value, quotePrice.Price.Value),
            () => Assert.Equal(alterPrice.Date, quotePrice.Price.Date));
    }

    [Fact]
    public void AlterPrice_PriceNull_ThrowsArgumentNullException()
    {
        var quotePrice = new QuotePrice(new(DefaultPrice, DateTime.Now.AddSeconds(-30)));

        var exception = Assert.Throws<ArgumentNullException>(() => quotePrice.AlterPrice(null!));
        Assert.Equal("O valor da cotação alterado não pode ser nulo. (Parameter 'price')", exception.Message);
        Assert.NotNull(quotePrice.Price);
    }
}
