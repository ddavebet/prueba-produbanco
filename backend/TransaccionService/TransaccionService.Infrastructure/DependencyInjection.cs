using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransaccionService.Application.Abstractions.Services;
using TransaccionService.Infrastructure.Services;

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

            services.AddHttpClient<IProductoClient, ProductoClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["Services:ProductoService"]);
            });

            return services;
        }
    }
}
