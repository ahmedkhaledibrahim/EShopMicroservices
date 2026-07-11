using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
