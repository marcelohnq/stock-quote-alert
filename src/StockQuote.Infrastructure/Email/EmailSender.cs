using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using StockQuote.Core.Interfaces;
using System.Net;
using System.Net.Mail;

namespace StockQuote.Infrastructure.Email;

public class EmailSender(IConfiguration _config) : IEmailSender
{
    private readonly string _smtpHost = Guard.Against.NullOrWhiteSpace(_config["Email:SmtpHost"]);
    private readonly string _smtpPort = Guard.Against.NullOrWhiteSpace(_config["Email:SmtpPort"]);
    private readonly string _smtpUser = Guard.Against.NullOrWhiteSpace(_config["Email:SmtpUser"]);
    private readonly string _smtpPass = Guard.Against.NullOrWhiteSpace(_config["Email:SmtpToken"]);
    private readonly string _toEmail = Guard.Against.NullOrWhiteSpace(_config["Email:To"]);

    public async Task SendEmailAsync(string subject, string body)
    {
        if (int.TryParse(_smtpPort, out int port))
        {
            return;
        }

        using var smtpClient = new SmtpClient(_smtpHost, port)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(_smtpUser, _toEmail, subject, body);
        await smtpClient.SendMailAsync(mailMessage);
    }
}
