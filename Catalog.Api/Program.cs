using Carter;
using Catalog.Api.Middlewares;
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
}).UseDirtyTrackedSessions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseErrorHandlerMiddleware();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter();

app.Run();
