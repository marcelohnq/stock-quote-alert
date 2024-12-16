using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockQuote.Core.QuoteAggregate;

namespace StockQuote.Infrastructure.Data.Config;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Quote.Prices));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(q => q.Asset);
        builder.OwnsOne(q => q.LimitAlert);
        builder.OwnsOne(q => q.Audit);
    }
}

