using Discount.gRPC.Data;
using Discount.gRPC.Extensions;
using Discount.gRPC.Services;
using Mapster;
using System.Reflection;
using FluentValidation;
using Discount.gRPC.Interceptors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options => {
    options.EnableDetailedErrors = true;
    options.Interceptors.Add<ValidationInterceptor>();
});
builder.Services.AddGrpcReflection();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
TypeAdapterConfig.GlobalSettings.Scan(typeof(IRegister).Assembly);
var app = builder.Build();
//app.UseSeedingMigration();
app.MapGrpcService<DiscountService>();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
app.Run();
