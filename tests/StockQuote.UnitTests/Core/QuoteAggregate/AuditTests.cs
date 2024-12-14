namespace StockQuote.UnitTests.Core.QuoteAggregate;

public class AuditTests
{
    [Fact]
    public void Constructor_Audit_SuccessCreate()
    {
        var audit = new Audit();

        Assert.Multiple(
            () => Assert.Equal(DateTime.Now.ToShortDateString(), audit.CreatedAt.ToShortDateString()),
            () => Assert.Equal(DateTime.Now.ToShortDateString(), audit.UpdatedAt.ToShortDateString()));
    }
}
