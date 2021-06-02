using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace.infrastructure.EntityTypeConfigurations
{
    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("PictureId")
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(x=>x.ParentId);
            builder.OwnsOne(x => x.Size);
        }
    }
}