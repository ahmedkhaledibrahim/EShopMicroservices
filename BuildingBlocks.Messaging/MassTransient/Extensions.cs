using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransient
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection service, IConfiguration configuration,Assembly? assembly = null) {
            service.AddMassTransit(config =>
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
                    configurator.Message<BasketCheckoutEvent>(m =>
                    {
                        m.SetEntityName("basket-checkout");
                    });
                    configurator.Publish<BasketCheckoutEvent>(m =>
                    {
                        m.ExchangeType = "direct";
                    });
                    
                    //configurator.ConfigureEndpoints(context);
                });
            });
            return service;
        }
    }
}
