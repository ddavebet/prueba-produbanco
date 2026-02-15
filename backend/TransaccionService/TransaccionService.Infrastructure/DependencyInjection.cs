using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TransaccionService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<TransaccionDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TransaccionDb"))
            );

            return services;
        }
    }
}
