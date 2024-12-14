namespace StockQuote.Core.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string subject, string body);
}
