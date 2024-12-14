namespace StockQuote.Core.BaseClasses;

public abstract class EntityBase : HasDomainEvents
{
    public int Id { get; set; }
}