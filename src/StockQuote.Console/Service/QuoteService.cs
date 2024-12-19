using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace StockQuote.Console.Service;

public partial class QuoteService(IMediator _mediator,
    IConfiguration _config,
    ILogger<QuoteService> _logger) : BackgroundService
{
    private readonly string[] _commandLineArgs = Environment.GetCommandLineArgs();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Serviço de Cotação - Iniciado.");

        if (!CommandLineValidate())
        {
            _logger.LogInformation("Os argumentos informados não possuem o padrão esperado (ex: PETR4 22.67 22.59).");
            return; // Close BackgroundService
        }

        // Simulação de trabalho
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation("Serviço de Cotação - Finalizado.");
    }

    private bool CommandLineValidate()
    {
        if (_commandLineArgs is null) return false;
        if (_commandLineArgs.Length != 4) return false;
        if (!TickerRegex().IsMatch(_commandLineArgs[1])) return false;
        if (!decimal.TryParse(_commandLineArgs[2], out decimal _)) return false;
        if (!decimal.TryParse(_commandLineArgs[3], out decimal _)) return false;

        return true;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9]{5,6}$")]
    private static partial Regex TickerRegex();
}