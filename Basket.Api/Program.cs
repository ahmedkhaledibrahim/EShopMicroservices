using Basket.Api.Data.Seeding;
using Basket.Api.Extensions;
using Basket.Api.Features.Commands.CreateBasket;
using Carter;
using JasperFx;
using Marten;
using MediatR;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.AutoCreateSchemaObjects = AutoCreate.All;
    options.DatabaseSchemaName = "eshop";
}).UseDirtyTrackedSessions();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddGrpcClientConfigurations(builder.Configuration);
builder.Services.AddServices();

if (builder.Environment.IsDevelopment()) { 
   builder.Services.InitializeMartenWith(new InitialSeeding(InitialDataSets.shoppingCarts));
}
builder.Services.AddCarter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter();

app.Run();

