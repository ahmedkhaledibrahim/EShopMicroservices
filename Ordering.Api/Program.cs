using Ordering.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();
app.Run();

