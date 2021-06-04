using System;
using System.Threading.Tasks;
using marketplace.domain;
using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;
using marketplace.infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace marketplace.infrastructure
{
    public class ClassifiedAdContext:DbContext,IUnitOfWork
    {
        private readonly ILoggerFactory _logger;

        public DbSet<ClassifiedAd> ClassifiedAds{ get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        // public DbSet<Picture> Pictures{ get; set; }

        public ClassifiedAdContext(DbContextOptions<ClassifiedAdContext> options):base(options)
        {

        }

        public ClassifiedAdContext(DbContextOptions<ClassifiedAdContext> options,ILoggerFactory logger):this(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
        }


        public Task Commit()
        {
            return base.SaveChangesAsync();
        }


    }

    public class DataContextDesignFactory:IDesignTimeDbContextFactory<ClassifiedAdContext>
    {
        public ClassifiedAdContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClassifiedAdContext>();
            optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1;Initial Catalog=ClassifiedAd;Persist Security Info=True;User ID=SA;password=Heip5375;");
            return new ClassifiedAdContext(optionsBuilder.Options);
        }
    }
}
