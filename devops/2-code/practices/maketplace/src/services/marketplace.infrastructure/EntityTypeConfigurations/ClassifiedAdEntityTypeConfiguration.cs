using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace.infrastructure.EntityTypeConfigurations
{
    public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.OwnsOne(x => x.OwnerId)
                .Property(x=>x.Value)
                .HasColumnName("OwnerId")
                .HasMaxLength(50);
            
            builder.OwnsOne(x => x.Price
                , price => { 
                    price.Property(x=>x.Amount)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(18, 2");

                    price.OwnsOne(c => c.Currency,currency=>
                    {
                        currency.Property(x => x.CurrencyCode)
                        .HasColumnName("CurrencyCode")
                        .HasMaxLength(10);
                        currency.Property(x => x.InUse)
                        .HasColumnName("InUse")
                        .HasColumnType("bit");
                        currency.Property(x => x.DecimalPlace)
                        .HasColumnName("DecimalPlace")
                        .HasColumnType("decimal(2, 0)");
                    });
                });
            
            builder.OwnsOne(x => x.Text)
                .Property(x=>x.Value)
                .HasColumnName("Text")
                .HasMaxLength(150);
            
            builder.OwnsOne(x => x.Title)
                .Property(x=>x.Value)
                .HasColumnName("Title")
                .HasMaxLength(50);
            
            builder.OwnsOne(x => x.ApprovedBy);

            builder.HasMany(c => c.Pictures)
            .WithOne()
            .HasForeignKey("ParentId")
            .OnDelete(DeleteBehavior.Cascade);;

            var navigation = builder.Metadata.FindNavigation(nameof(ClassifiedAd.Pictures));

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetField("_pictures");
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}