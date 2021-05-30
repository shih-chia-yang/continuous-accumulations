using marketplace.infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace marketplace.api.Registry
{
    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddDbContext<ClassifiedAdContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}