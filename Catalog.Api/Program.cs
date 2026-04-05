using Catalog.Application;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Queries.GetAllProducts;
using Catalog.Application.Features.Products.Queries.GetProductByID;
using Catalog.Domain.Entities;
using JasperFx;
using JasperFx.Events.Daemon;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddMarten(options => { 
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.AutoCreateSchemaObjects = AutoCreate.All;
    options.DatabaseSchemaName = "eshop";
    options.Schema.For<Product>().Identity(x => x.ID);
});
var app = builder.Build();

app.MapPost("/products", async (CreateProductCommand command, IMediator mediator) =>
{
    var response = await mediator.Send(command);
    return Results.Ok(response);
});

app.MapGet("/products", async (IMediator mediator) =>
{
    var response = await mediator.Send(new GetAllProductsRequest());
    return Results.Ok(response);
});
app.MapGet("/products/{id}", async (IMediator mediator, Guid id) =>
{
    var response = await mediator.Send(new GetProductByIDRequest { 
      ID = id
    });
    return Results.Ok(response);
});

app.Run();
