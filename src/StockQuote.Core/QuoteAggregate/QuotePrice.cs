using Ardalis.GuardClauses;
using StockQuote.Core.BaseClasses;

namespace StockQuote.Core.QuoteAggregate;

public class QuotePrice : EntityBase
{
    public Price Price { get; private set; }

#pragma warning disable CS8618 // Used by EF
    private QuotePrice() { }

    public QuotePrice(Price price)
    {
        Price = Guard.Against.Null(price, message: "O valor da cotação informado não pode ser nulo.");
    }

    public void AlterPrice(Price price) =>
        Price = Guard.Against.Null(price, message: "O valor da cotação alterado não pode ser nulo.");
}

public class Price
{
    public decimal Value { get; private set; }
    public DateTime Date { get; private set; }

    private Price() { } // Used by EF

    public Price(decimal price, DateTime date)
    {
        Value = Guard.Against.NegativeOrZero(price, message: "É necessário informar o valor da cotação.");
        Date = Guard.Against.InvalidInput(date, nameof(date), d => d >= DateTime.Now.Date, "A data da cotação informada é inválida.");
    }
}