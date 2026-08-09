using Basket.Api.Features.EventHandlers;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using System.Reflection;

namespace Basket.Api
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddMessageBrokerServices(this IServiceCollection service, IConfiguration configuration, Assembly? assembly = null) {
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
                    configurator.ReceiveEndpoint("order-created-queue", e => {
                        e.ConfigureConsumer<OrderCreatedIntegrationEventHandler>(context);
                    });
                });
            });
            return service;
        }
    }
}
