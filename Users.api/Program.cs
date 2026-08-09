using Carter;
using Users.api.Extensions;
using Users.Application;
using Users.Persistence;
using Users.Persistence.Settings;
using Users.Services;
using Users.Services.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddUsersServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddEndpointsApiExplorer();

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
app.UseErrorHandlingMiddleware();
app.UseCors("AllowAll");
app.MapCarter();

app.Run();
