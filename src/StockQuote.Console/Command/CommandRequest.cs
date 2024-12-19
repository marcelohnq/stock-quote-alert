using MediatR;
using Microsoft.Extensions.Logging;
using StockQuote.UseCases.Quotes.List;

namespace StockQuote.Console.Command;

public class CommandRequest(IMediator _mediator, ILogger<CommandRequest> _logger)
{
    public async Task ExecuteCommand(string[] args)
    {
        switch (args[0])
        {
            case "-l":
                await _mediator.Send(new ListQuotesQuery());
                break;
        }
    }
}
