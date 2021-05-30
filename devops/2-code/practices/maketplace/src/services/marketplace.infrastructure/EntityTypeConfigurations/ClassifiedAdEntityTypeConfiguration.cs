using marketplace.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace.infrastructure.EntityTypeConfigurations
{
    public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.OwnsOne(x => x.OwnerId)
                .Property(x=>x.Value).HasMaxLength(50);
            builder.OwnsOne(x => x.Price
                , price => { 
                    price.Property(x=>x.Amount).HasColumnType("decimal(18, 2");
                    price.OwnsOne(c => c.Currency,currency=>
                    {
                        currency.Property(x => x.CurrencyCode).HasMaxLength(10);
                        currency.Property(x => x.InUse).HasColumnType("bit");
                        currency.Property(x => x.DecimalPlace).HasColumnType("decimal(2, 0)");
                    });
                });
            builder.OwnsOne(x => x.Text)
                .Property(x=>x.Value).HasMaxLength(150);
            builder.OwnsOne(x => x.Title)
                .Property(x=>x.Value).HasMaxLength(50);
            builder.OwnsOne(x => x.ApprovedBy);
        }
    }
}