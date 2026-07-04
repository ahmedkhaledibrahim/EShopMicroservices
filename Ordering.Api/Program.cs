using Ordering.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();
app.Run();

