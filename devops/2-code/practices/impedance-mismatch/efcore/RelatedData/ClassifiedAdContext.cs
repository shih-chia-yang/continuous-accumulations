using efcore.RelatedData.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace efcore.RelatedData
{
    public class ClassifiedAdContext:DbContext
    {
        public DbSet<ClassifiedAd> ClassifiedAds { get; set; }

        public DbSet<Picture> Pictures { get; set; }
        public ClassifiedAdContext(){}
        public ClassifiedAdContext(DbContextOptions<ClassifiedAdContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1;Initial Catalog=ClassifiedAd-ef;Persist Security Info=True;User ID=SA;password=qwer%TGB;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    public class DataContextDesignFactory:IDesignTimeDbContextFactory<ClassifiedAdContext>
    {
        public ClassifiedAdContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClassifiedAdContext>();
            optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1;Initial Catalog=ClassifiedAd-ef;Persist Security Info=True;User ID=SA;password=qwer%TGB;");
            return new ClassifiedAdContext(optionsBuilder.Options);
        }
    }
}