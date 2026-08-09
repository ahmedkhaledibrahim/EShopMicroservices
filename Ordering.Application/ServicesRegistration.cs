using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.MassTransient;
using FluentValidation;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using Ordering.Application.Features.Orders.EventHandlers.Integration;
using System.Reflection;

namespace Ordering.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            services.AddOrderingMessageBroker(configuration, Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddOrderingMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null) {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                if (assembly != null)
                {
                    config.AddConsumers(assembly);
                }
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configuration["MessageBroker:Username"]);
                        host.Password(configuration["MessageBroker:Password"]);
                    });
                  
                    configurator.ReceiveEndpoint("basket-checkout-queue", e =>
                    {
                        e.Bind("basket-checkout", x =>
                        {
                            x.ExchangeType = "direct";
                            x.RoutingKey = "basket-checkout";
                            
                        });
                        e.ConfigureConsumer<BasketCheckoutEventHandler>(context);
                    });
                    
                });
            });
            return services;
        }
    }
}
