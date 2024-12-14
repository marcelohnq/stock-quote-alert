﻿using Ardalis.GuardClauses;
using StockQuote.Core.BaseClasses;
using StockQuote.Core.Interfaces;

namespace StockQuote.Core.QuoteAggregate;

public class Quote : EntityBase, IAggregateRoot
{
    private readonly List<QuotePrice> _prices = [];

    public Asset Asset { get; private set; }
    public LimitAlert LimitAlert { get; private set; }
    public Audit Audit { get; private set; } = new();

    public IReadOnlyCollection<QuotePrice> Prices => _prices.AsReadOnly();

    public Quote(Asset asset, LimitAlert limit)
    {
        Asset = Guard.Against.Null(asset, message: "É necessário informar um ativo.");
        LimitAlert = Guard.Against.Null(limit, message: "É necessário informar o limite de alerta para a cotação.");
    }

    public void AddPrice(QuotePrice price)
    {
        Guard.Against.Null(price, message: "O preço precisa ser informado.");
        _prices.Add(price);
    }
}

public class Asset
{
    public string Ticker { get; private set; }

    public Asset(string ticker)
    {
        Guard.Against.NullOrWhiteSpace(ticker, message: "É necessário informar o ticker do ativo.");
        Ticker = Guard.Against.InvalidFormat(ticker, nameof(ticker), @"^[a-zA-Z0-9]{5,6}$", "O ticker informado possui um formato inválido.");
        Ticker = Ticker.Trim().ToUpper();
    }
}

public class LimitAlert(decimal up, decimal down)
{
    public decimal Up { get; private set; } = Guard.Against.NegativeOrZero(up, message: "É necessário informar o limite superior para o alerta.");
    public decimal Down { get; private set; } = Guard.Against.NegativeOrZero(down, message: "É necessário informar o limite inferior para o alerta.");
}
