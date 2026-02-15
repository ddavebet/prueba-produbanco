using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductoService.Domain.IRepositories;
using ProductoService.Infrastructure.Repositories;

namespace ProductoService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<ProductoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProductoDb"))
            );

            services.AddScoped<IProductoRepository, ProductoRepository>();

            return services;
        }
    }
}
