using Microsoft.EntityFrameworkCore;
using Shortly.Infrastructure.Persistence.DbContexts;
using Shortly.Presentation.Common.Mappers;

namespace Shortly.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddScoped<UrlMapper>();

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