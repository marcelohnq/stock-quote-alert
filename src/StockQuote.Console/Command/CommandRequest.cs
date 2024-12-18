using MediatR;
using Microsoft.Extensions.Logging;
using StockQuote.UseCases.Quotes.List;

namespace StockQuote.Console.Command;

public class CommandRequest(IMediator _mediator, ILogger<CommandRequest> _logger)
{
    private readonly string[] commandLineArgs = Environment.GetCommandLineArgs();

    public async Task ExecuteCommand()
    {
        switch (commandLineArgs[1])
        {
            case "-l":
                await _mediator.Send(new ListQuotesQuery());
                break;
        }
    }
}
