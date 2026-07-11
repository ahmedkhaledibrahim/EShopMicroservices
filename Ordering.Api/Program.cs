using Ordering.Persistence;
using Ordering.Application;
using Carter;
using Ordering.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
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
if (builder.Environment.IsDevelopment())
{
    await MigrationRegistrations.SeedDataAsync(app.Services);
}
app.UseErrorHandlingMiddleware();
app.UseHttpsRedirection();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter();
app.Run();

