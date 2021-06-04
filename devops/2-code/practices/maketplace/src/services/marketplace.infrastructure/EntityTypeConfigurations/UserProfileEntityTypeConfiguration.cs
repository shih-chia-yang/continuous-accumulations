using marketplace.domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace.infrastructure.EntityTypeConfigurations
{
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("UserProfileId")
                .ValueGeneratedNever()
                .IsRequired();

            builder.OwnsOne(x => x.FullName,f=>{
                f.Property(x => x.Value)
                .HasColumnName("FullName")
                .HasMaxLength(100);
            });
            builder.OwnsOne(x => x.DisplayName,d=>{
                d.Property(x => x.Value)
                .HasColumnName("DisplayName")
                .HasMaxLength(150);
            });

            builder.Property(x => x.PhotoUrl)
            .HasMaxLength(400);
        }
    }
}