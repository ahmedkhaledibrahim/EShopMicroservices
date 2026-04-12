using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(Behaviours.LoggingBehaviour<,>));
            });
            services.AddMapster();
            return services;
            
        }
    }
}
