using Microsoft.EntityFrameworkCore;
using Shortly.Infrastructure.Persistence.DbContexts;

namespace Shortly.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static void ApplyDbMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ShortUrlDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ShortUrlDbContext>();

            dbContext.Database.Migrate();
        }
    }
}