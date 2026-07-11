using Mapster;
using Ordering.Application.Features.Orders.Common;
using Ordering.Application.Features.Orders.Queries.GetOrders;
using Ordering.Domain.Aggregates;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.MappingConfig
{
    public sealed class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddressDto, Address>()
                .MapWith(src => Address.Of(src.AddressLine, src.City, src.State, src.PostalCode));

            config.NewConfig<PaymentDto, Payment>()
                .MapWith(src => Payment.Of(src.CardName, src.CardNumber, src.Expiration, src.CVV, src.PaymentMethod));

            config.NewConfig<OrderItemInputDto, Price>()
                .MapWith(src => Price.Of(src.Price, src.Currency));

            config.NewConfig<Address, AddressDto>();

            config.NewConfig<Payment, PaymentDto>();

            config.NewConfig<OrderItem, OrderItemDto>()
                .Map(dest => dest.Id, src => src.ID.Value)
                .Map(dest => dest.ProductId, src => src.ProductId.Value)
                .Map(dest => dest.Price, src => src.Price.Value)
                .Map(dest => dest.Currency, src => src.Price.Currency);

            config.NewConfig<Order, OrderResponse>()
                 .MapWith(src => new OrderResponse {
                     Id = src.ID.Value,
                     CustomerId = src.CustomerId.Value,
                     OrderName = src.OrderName.Value,
                     ShippingAddress = src.ShippingAddress.Adapt<AddressDto>(config),
                     BillingAddress = src.BillingAddress.Adapt<AddressDto>(config),
                     Payment = src.Payment.Adapt<PaymentDto>(config),
                     OrderItems = src.OrderItems.Adapt<List<OrderItemDto>>(config),
                     Status = src.Status.ToString(),
                     TotalPrice = src.TotalPrice }
                 );
        }
    }
}
