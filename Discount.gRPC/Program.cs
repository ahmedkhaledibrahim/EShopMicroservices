
using Discount.gRPC.Data;
using Discount.gRPC.Extensions;
using Discount.gRPC.MappingConfig;
using Discount.gRPC.Services;
using Mapster;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options => options.EnableDetailedErrors = true);
builder.Services.AddGrpcReflection();
builder.Services.AddPersistenceServices(builder.Configuration);
TypeAdapterConfig.GlobalSettings.Scan(typeof(CouponsMappingConfig).Assembly);
var app = builder.Build();
app.UseSeedingMigration();
app.MapGrpcService<DiscountService>();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
app.Run();
