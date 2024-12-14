namespace StockQuote.Core.BaseClasses;

public class Audit
{
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
}
