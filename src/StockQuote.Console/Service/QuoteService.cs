using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuote.Core.Interfaces;
using StockQuote.UseCases.Quotes;
using StockQuote.UseCases.Quotes.Alert;
using StockQuote.UseCases.Quotes.Create;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StockQuote.Console.Service;

public partial class QuoteService(IServiceScopeFactory _serviceScopeFactory,
    IApiQuote _apiQuote,
    IConfiguration _config,
    ILogger<QuoteService> _logger) : BackgroundService
{
    private readonly string[] _commandLineArgs = Environment.GetCommandLineArgs();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Serviço de Cotação - Iniciado.");

        if (!CommandLineValidate(out string ticker, out decimal up, out decimal down))
        {
            _logger.LogInformation("Os argumentos informados não possuem o padrão esperado (ex: PETR4 22.67 22.59).");
            return; // Close BackgroundService
        }

        var currentPrice = await _apiQuote.GetCurrentQuote(ticker);

        if (currentPrice is null)
        {
            _logger.LogInformation("Não foi possível capturar a cotação do ativo {Ticker}.", ticker);
            return;
        }

        var quote = await CreateAssetQuote(ticker, up, down);

        if (quote is null)
        {
            _logger.LogInformation("Não foi possível criar a cotação do ativo {Ticker}.", ticker);
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            currentPrice = await _apiQuote.GetCurrentQuote(ticker);
            await AlertQuotePrice(quote.Id, currentPrice.Value);

            await Task.Delay(60000, stoppingToken);
        }

        _logger.LogInformation("Serviço de Cotação - Finalizado.");
    }

    private bool CommandLineValidate(out string ticker, out decimal up, out decimal down)
    {
        ticker = string.Empty;
        up = default;
        down = default;

        if (_commandLineArgs is null) return false;
        if (_commandLineArgs.Length != 4) return false;
        if (!TickerRegex().IsMatch(_commandLineArgs[1])) return false;
        if (!decimal.TryParse(_commandLineArgs[2], CultureInfo.InvariantCulture, out up)) return false;
        if (!decimal.TryParse(_commandLineArgs[3], CultureInfo.InvariantCulture, out down)) return false;

        ticker = _commandLineArgs[1];
        up = TruncateN2(up);
        down = TruncateN2(down);

        return true;
    }

    private async Task<QuoteDto?> CreateAssetQuote(string ticker, decimal up, decimal down)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(new CreateQuoteCommand(ticker, up, down));
    }

    private async Task<bool> AlertQuotePrice(int idQuote, decimal price)
    {
        price = TruncateN2(price);

        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(new AlertQuoteCommand(idQuote, price, DateTime.Now));
    }

    private static decimal TruncateN2(decimal value) =>
        Math.Truncate(value * 100) / 100;

    [GeneratedRegex(@"^[a-zA-Z0-9]{5,6}$")]
    private static partial Regex TickerRegex();
}