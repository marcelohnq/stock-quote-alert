using MediatR;
using Microsoft.Extensions.Logging;
using StockQuote.Core.Interfaces;
using StockQuote.Core.QuoteAggregate.Events;

namespace StockQuote.Core.QuoteAggregate.Handlers;

internal class PriceAddedHandler(IEmailSender emailSender,
    ILogger<PriceAddedHandler> logger) : INotificationHandler<PriceAddedEvent>
{
    private const string SubjectFormat = "Recomendação {0} - {1}";
    private const string BodyFormat = "O preço do ativo {0} atingiu o limite definido!\nLimites: {1}\nData: {2}\nPreço Atual: {3}\nRecomendação: {4}";

    public async Task Handle(PriceAddedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Enviando recomendação por e-mail do ativo {Ticker}", domainEvent.AssetTicker);

        var messageEmail = CreateEmail(domainEvent);

        await emailSender.SendEmailAsync(messageEmail.Item1, messageEmail.Item2);
    }

    private static (string, string) CreateEmail(PriceAddedEvent domainEvent)
    {
        var recommendation = domainEvent.CurrentPrice > domainEvent.LimitUp ? "Venda" : "Compra";

        var subject = string.Format(SubjectFormat, domainEvent.AssetTicker, recommendation);
        var body = string.Format(BodyFormat,
            domainEvent.AssetTicker,
            $"{domainEvent.LimitDown:C} - {domainEvent.LimitUp:C}",
            domainEvent.DatePrice.ToString("G"),
            domainEvent.CurrentPrice.ToString("C"),
            recommendation);

        return (subject, body);
    }
}