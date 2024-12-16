using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockQuote.Core.QuoteAggregate;

namespace StockQuote.Infrastructure.Data.Config;

public class QuotePriceConfiguration : IEntityTypeConfiguration<QuotePrice>
{
    public void Configure(EntityTypeBuilder<QuotePrice> builder)
    {
        builder.OwnsOne(qp => qp.Price);
    }
}
