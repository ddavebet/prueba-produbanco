using System.Net.Http.Json;
using TransaccionService.Application.Abstractions.Services;
using TransaccionService.Application.DTOs;

namespace TransaccionService.Infrastructure.Services
{
    public class ProductoClient : IProductoClient
    {
        private readonly HttpClient _httpClient;

        public ProductoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductoInfoDto>> ObtenerProductosPorIdsAsync(
            IEnumerable<Guid> productoIds
        )
        {
            var response = await _httpClient.PostAsJsonAsync("api/productos/batch", productoIds);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductoInfoDto>>() ?? [];
        }

        public async Task<bool> ExisteProductoAsync(Guid productoId)
        {
            var response = await _httpClient.GetAsync($"api/productos/{productoId}");

            return response.IsSuccessStatusCode;
        }

        public async Task AumentarStockAsync(Guid productoId, int cantidad)
        {
            var response = await _httpClient.PatchAsJsonAsync(
                $"api/productos/{productoId}/aumentar-stock",
                cantidad
            );

            response.EnsureSuccessStatusCode();
        }

        public async Task DisminuirStockAsync(Guid productoId, int cantidad)
        {
            var response = await _httpClient.PatchAsJsonAsync(
                $"api/productos/{productoId}/disminuir-stock",
                cantidad
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
