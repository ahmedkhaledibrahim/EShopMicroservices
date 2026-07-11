using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Persistence.Context;
using Ordering.Persistence.Data;
using Ordering.Persistence.Interceptors;
using Ordering.Persistence.Repositories;

namespace Ordering.Persistence
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DispatchDomainEventsInterceptor>();
            services.AddScoped<AuditInterceptor>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CustomerDataSeeder>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(
                    sp.GetRequiredService<DispatchDomainEventsInterceptor>(),
                    sp.GetRequiredService<AuditInterceptor>()
                    
                );
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }

        
    }

    public static class MigrationRegistrations {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //await context.Database.EnsureCreatedAsync();
            var customerSeeder = scope.ServiceProvider.GetRequiredService<CustomerDataSeeder>();
            await customerSeeder.SeedAsync();
        }
    }
}
