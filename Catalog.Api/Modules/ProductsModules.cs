using Carter;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Commands.DeleteProduct;
using Catalog.Application.Features.Products.Commands.UpdateProduct;
using Catalog.Application.Features.Products.Queries.GetAllProducts;
using Catalog.Application.Features.Products.Queries.GetProductByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Modules
{
    public sealed class ProductsModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products");
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllProductsRequest());
                return Results.Ok(result);
            });
            group.MapGet("/{id}", async (IMediator mediator, Guid id) =>
            { 
                 var result = await mediator.Send(new GetProductByIDRequest { ID = id });
                 return Results.Ok(result);
            });
            group.MapPost("/", async (IMediator mediator, CreateProductCommand request) => { 
                 var result = await mediator.Send(request);
                 return Results.Ok(result);
            });
            group.MapPut("/", async (IMediator mediator, UpdateProductCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
            group.MapDelete("/{id}", async (IMediator mediator,[FromRoute] Guid id) =>
            {
                var result = await mediator.Send(new DeleteProductCommand { ID = id });
                return Results.Ok(result);
            });
        }
    }
}
