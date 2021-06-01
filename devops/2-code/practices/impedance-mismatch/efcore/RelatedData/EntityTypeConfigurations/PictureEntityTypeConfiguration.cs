using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace efcore.RelatedData.EntityTypeConfigurations
{
    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.PictureId);
            builder.Property(x => x.PictureId)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(x => x.Width);
            builder.Property(x => x.Height);
            builder.Property(x => x.Location)
                .HasMaxLength(250);
            builder.Property(x => x.Order);
        }
    }
}