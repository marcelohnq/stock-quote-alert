using StockQuote.Core.QuoteAggregate;

namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class LimitAlertTests
{
    private const decimal DefaultUp = 38.15M;
    private const decimal DefaultDown = 38.07M;

    [Fact]
    public void Constructor_LimitAlert_SuccessCreate()
    {
        var limit = new LimitAlert(DefaultUp, DefaultDown);

        Assert.Multiple(
            () => Assert.Equal(DefaultUp, limit.Up),
            () => Assert.Equal(DefaultDown, limit.Down));
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public void Constructor_LimitAlertUpInvalid_ThrowsArgumentException(decimal up)
    {
        var exception = Assert.Throws<ArgumentException>(() => new LimitAlert(up, DefaultDown));
        Assert.Equal("É necessário informar o limite superior para o alerta. (Parameter 'up')", exception.Message);
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public void Constructor_LimitAlertDownInvalid_ThrowsArgumentException(decimal down)
    {
        var exception = Assert.Throws<ArgumentException>(() => new LimitAlert(DefaultUp, down));
        Assert.Equal("É necessário informar o limite inferior para o alerta. (Parameter 'down')", exception.Message);
    }
}
