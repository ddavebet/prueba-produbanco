using Microsoft.Extensions.DependencyInjection;
using ProductoService.Application.Features.Commands;

namespace ProductoService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CrearProductoHandler>();
            services.AddScoped<ActualizarProductoHandler>();
            services.AddScoped<EliminarProductoHandler>();

            return services;
        }
    }
}
