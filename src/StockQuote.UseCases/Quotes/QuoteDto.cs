namespace StockQuote.UseCases.Quotes;

public record QuoteDto(int Id, string Ticker, decimal LimitUp, decimal LimitDown, decimal? LastPrice);
