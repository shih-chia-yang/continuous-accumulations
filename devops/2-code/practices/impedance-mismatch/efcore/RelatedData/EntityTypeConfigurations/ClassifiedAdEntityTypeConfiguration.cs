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
            classifiedAd.Property(x => x.OwnerId);
            classifiedAd.Property(x => x.Title);
            classifiedAd.Property(x => x.Text);
            classifiedAd.Property(x => x.CurrencyCode);
            classifiedAd.Property(x => x.InUse);
            classifiedAd.Property(x => x.DecimalPlace);
            classifiedAd.Property(x => x.Amount);
            classifiedAd.Property(x => x.ApproveBy);
            classifiedAd.HasMany(c => c.Pictures)
               .WithOne()
               .HasForeignKey("ClassifiedAdId")
               .OnDelete(DeleteBehavior.Cascade);

            var navigation = classifiedAd.Metadata.FindNavigation(nameof(ClassifiedAd.Pictures));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}