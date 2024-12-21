using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockQuote.Core.Interfaces;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace StockQuote.Infrastructure.API;

public class ApiQuoteService : IApiQuote
{
    private readonly string _urlQuote;
    private readonly HttpClient _client;
    private readonly ILogger<ApiQuoteService> _logger;

    public ApiQuoteService(HttpClient client, IConfiguration config, ILogger<ApiQuoteService> logger)
    {
        _client = client;
        _logger = logger;
        _urlQuote = Guard.Against.NullOrWhiteSpace(config["APIQuote:URLEndpoint"]);

        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
    }

    public async Task<decimal?> GetCurrentQuote(string ticker)
    {
        var result = await _client.GetAsync(UrlTickerParam(ticker));

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError("Não foi possível buscar a cotação do ativo {Ticker}", ticker);
            return null;
        }

        var text = await result.Content.ReadAsStringAsync();
        var jObject = JsonSerializer.Deserialize<JsonObject>(text);

        var regularPrice = jObject?["chart"]?["result"]?[0]?["meta"]?["regularMarketPrice"];

        return regularPrice is not null && decimal.TryParse(regularPrice.ToString(), CultureInfo.InvariantCulture, out decimal resultDecimal) ?
            resultDecimal :
            null;
    }

    private string UrlTickerParam(string ticker) =>
        $"{_urlQuote}/{ticker}.SA";
}
