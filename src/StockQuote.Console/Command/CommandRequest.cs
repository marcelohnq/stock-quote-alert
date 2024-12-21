using MediatR;
using Microsoft.Extensions.Logging;
using StockQuote.UseCases.Quotes;
using StockQuote.UseCases.Quotes.List;

namespace StockQuote.Console.Command;

public class CommandRequest(IMediator _mediator, ILogger<CommandRequest> _logger)
{
    private const string PrintFormat = "[{Id} - {Ticker}] {Down} ~ {Up} = {Price}";

    public async Task ExecuteCommand(string[] args)
    {
        switch (args[0])
        {
            case "-l":
                var list = await _mediator.Send(new ListQuotesQuery());
                PrintQuote(list ?? []);
                break;
        }
    }

    private void PrintQuote(IEnumerable<QuoteDto> list)
    {
        foreach (var item in list)
        {
            _logger.LogInformation(PrintFormat, item.Id, item.Ticker, item.LimitDown,
                item.LimitUp, item.LastPrice);
        }
    }
}
