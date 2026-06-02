using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DiscountContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
