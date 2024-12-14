using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class PriceTests
{
    private const decimal DefaultPrice = 38.44M;

    public static IEnumerable<object[]> DatesOld()
    {
        yield return [DateTime.MinValue];
        yield return [DateTime.Now.AddYears(-1)];
        yield return [DateTime.Now.AddMonths(-1)];
        yield return [DateTime.Now.AddDays(-1)];
    }

    [Fact]
    public void Constructor_Price_SuccessCreate()
    {
        var value = DefaultPrice;
        var date = DateTime.Now;

        var price = new Price(value, date);

        Assert.Multiple(
            () => Assert.Equal(value, price.Value),
            () => Assert.Equal(date, price.Date));
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public void Constructor_PriceNegativeOrZero_ThrowsArgumentException(decimal price)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Price(price, DateTime.Now));
        Assert.Equal("É necessário informar o valor da cotação. (Parameter 'price')", exception.Message);
    }

    [Theory]
    [MemberData(nameof(DatesOld))]
    public void Constructor_PriceDateOld_ThrowsArgumentException(DateTime date)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Price(DefaultPrice, date));
        Assert.Equal("A data da cotação informada é inválida. (Parameter 'date')", exception.Message);
    }
}
