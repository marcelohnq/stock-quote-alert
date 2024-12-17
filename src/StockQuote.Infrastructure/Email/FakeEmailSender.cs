using StockQuote.Core.Interfaces;

namespace StockQuote.Infrastructure.Email;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string subject, string body)
    {
        return Task.CompletedTask;
    }
}
