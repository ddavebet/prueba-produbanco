using TransaccionService.Application.DTOs;

namespace TransaccionService.Application.Abstractions.Services
{
    public interface IProductoClient
    {
        Task<IEnumerable<ProductoInfoDto>> ObtenerProductosPorIdsAsync(IEnumerable<Guid> productoIds);
        Task<ProductoDto> ObtenerProductoPorIdAsync(Guid productoId);
        Task AumentarStockAsync(Guid productoId, int cantidad);
        Task DisminuirStockAsync(Guid productoId, int cantidad);
    }
}
