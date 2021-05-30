using marketplace.infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace marketplace.api.Registry
{
    public static class AppBuilderExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService(typeof(ClassifiedAdContext)) as ClassifiedAdContext;
            if(!context.Database.EnsureCreated())
                context.Database.Migrate();
        }
    }
}