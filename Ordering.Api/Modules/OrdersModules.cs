using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.AddOrderItem;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.RemoveOrderItem;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrders;

namespace Ordering.Api.Modules
{
    public sealed class OrdersModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/orders");
            group.MapGet("/", async (IMediator mediator,[AsParameters] GetOrdersQuery query) =>
            {
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
            group.MapPost("/", async (IMediator mediator, [FromBody] CreateOrderCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
            group.MapPost("/add-order-item", async (IMediator mediator, [FromBody] AddOrderItemCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
            group.MapPut("/", async (IMediator mediator,[FromBody] UpdateOrderCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            group.MapDelete("/", async (IMediator mediator, [AsParameters] DeleteOrderCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
            group.MapDelete("/delete-order-item", async (IMediator mediator,[AsParameters] RemoveOrderItemCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });


        }
    }
}
