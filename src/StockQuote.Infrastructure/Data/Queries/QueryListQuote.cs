using Microsoft.EntityFrameworkCore;
using StockQuote.UseCases.Interfaces;
using StockQuote.UseCases.Quotes;

namespace StockQuote.Infrastructure.Data.Queries;

public class QueryListQuote(QuoteContext _dbContext) : IQueryListQuote
{
    private const string SqlConsult = @"
        WITH LatestPrices AS (
            SELECT 
                QuoteId,
                Price_Value,
                Price_Date,
                ROW_NUMBER() OVER (PARTITION BY QuoteId ORDER BY Price_Date DESC) AS rn
            FROM dbo.QuotePrice
        )
        SELECT 
            Q.Id AS Id,
            Q.Asset_Ticker AS Ticker,
            Q.LimitAlert_Up AS LimitUp,
            Q.LimitAlert_Down AS LimitDown,
            LP.Price_Value AS LastPrice
        FROM dbo.Quotes Q
        LEFT JOIN LatestPrices LP ON Q.Id = LP.QuoteId AND LP.rn = 1;
    ";

    public async Task<IEnumerable<QuoteDto>?> ListQuoteAsync(CancellationToken cancellationToken = default)
    {
        var list = await _dbContext.Database.SqlQuery<QuoteDto>($"{SqlConsult}")
            .ToListAsync(cancellationToken);

        return list;
    }
}
