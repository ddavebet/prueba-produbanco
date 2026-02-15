using Microsoft.Extensions.DependencyInjection;
using TransaccionService.Application.Features.Commands;
using TransaccionService.Application.Features.Queries;

namespace TransaccionService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CrearTransaccionHandler>();
            services.AddScoped<ObtenerTransaccionesHandler>();

            return services;
        }
    }
}
