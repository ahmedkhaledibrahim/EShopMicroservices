using Discount.gRPC.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Extensions
{
    public static class SeedingExtension
    {
        public static void UseSeedingMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DiscountContext>();
            context.Database.Migrate();
        }
    }
}
