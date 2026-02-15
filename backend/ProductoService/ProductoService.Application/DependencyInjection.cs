using Microsoft.Extensions.DependencyInjection;
using ProductoService.Application.Features.Commands;
using ProductoService.Application.Features.Queries;

namespace ProductoService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ObtenerProductosHandler>();
            services.AddScoped<ObtenerProductoPorIdHandler>();
            services.AddScoped<CrearProductoHandler>();
            services.AddScoped<ActualizarProductoHandler>();
            services.AddScoped<EliminarProductoHandler>();

            return services;
        }
    }
}
