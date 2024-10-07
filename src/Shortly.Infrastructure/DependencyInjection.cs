using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortly.Infrastructure.Persistence.DbContexts;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Infrastructure.Persistence.Repositories;

namespace Shortly.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddDbContext<ShortUrlDbContext>(options =>
                options.UseMySql(
                        configuration.GetConnectionString("DefaultMariaDb"),
                        new MySqlServerVersion(new Version(11, 5, 2)),
                        sqlOptions => sqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)
                    )
            );

            services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

            return services;
        }
    }
}