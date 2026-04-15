using Basket.Api.Features.Commands.CreateBasket;
using Basket.Api.Features.Queries.GetBasket;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Modules
{
    public class BasketModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/basket");
            group.MapGet("/", async (IMediator mediator,[AsParameters] GetBasketQuery query) =>
            {
                var response = await mediator.Send(query);
                return Results.Ok(response);
            });
            group.MapPost("/", async (IMediator mediator,[FromBody] CreateBasketCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
        }
    }
}
