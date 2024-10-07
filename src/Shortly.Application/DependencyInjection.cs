using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shortly.Application.Common.Behaviors;
using Shortly.Application.Common.Interfaces.Application.Services;
using Shortly.Application.ShortUrls.Services;
using System.Reflection;

namespace Shortly.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            services.AddScoped<IUrlShorteningService, UrlShorteningService>();

            return services;
        }
    }
}