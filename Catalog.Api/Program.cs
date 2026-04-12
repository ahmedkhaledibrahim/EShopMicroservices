using Carter;
using Catalog.Api.Middlewares;
using Catalog.Application;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Data;
using JasperFx;
using Marten;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.AutoCreateSchemaObjects = AutoCreate.All;
    options.DatabaseSchemaName = "eshop";
    options.Schema.For<Product>().Identity(x => x.ID);
}).UseDirtyTrackedSessions();
if (builder.Environment.IsDevelopment()) {
    builder.Services.InitializeMartenWith(new InitialSeededData(InitialDataSets.products));
}
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
