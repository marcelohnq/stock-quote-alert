using Ardalis.GuardClauses;
using StockQuote.Core.BaseClasses;

namespace StockQuote.Core.QuoteAggregate;

public class QuotePrice(Price price) : EntityBase
{
    public Price Price { get; private set; } = Guard.Against.Null(price, message: "O valor da cotação informado não pode ser nulo.");

    public void AlterPrice(Price price) =>
        Price = Guard.Against.Null(price, message: "O valor da cotação alterado não pode ser nulo.");
}

public class Price(decimal price, DateTime date)
{
    public decimal Value { get; private set; } =
        Guard.Against.NegativeOrZero(price, message: "É necessário informar o valor da cotação.");
    public DateTime Date { get; private set; } =
        Guard.Against.InvalidInput(date, nameof(date), d => d >= DateTime.Now.Date, "A data da cotação informada é inválida.");
}