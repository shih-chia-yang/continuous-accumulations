using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace efcore.RelatedData.EntityTypeConfigurations
{
    public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<ClassifiedAd> classifiedAd)
        {
            classifiedAd.HasKey(x => x.ClassifiedAdId);
            classifiedAd.Property(x => x.ClassifiedAdId)
                .ValueGeneratedNever()
                .IsRequired();

            classifiedAd.Property(x => x.OwnerId);
            classifiedAd.Property(x => x.Title)
                .HasMaxLength(150);
            classifiedAd.Property(x => x.Text)
                .HasMaxLength(400);
            classifiedAd.Property(x => x.CurrencyCode)
                .HasMaxLength(4);
            classifiedAd.Property(x => x.InUse);
            classifiedAd.Property(x => x.DecimalPlace)
                .HasColumnType("decimal(2,0)");
            classifiedAd.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)");
            classifiedAd.Property(x => x.ApproveBy);
            classifiedAd.HasMany(c => c.Pictures)
               .WithOne()
               .HasForeignKey("ClassifiedAdId")
               .OnDelete(DeleteBehavior.Cascade);

            var navigation = classifiedAd.Metadata
                .FindNavigation(nameof(ClassifiedAd.Pictures));
            navigation.SetField("_picture");
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}