using ProductoService.Application.DTOs;
using ProductoService.Domain.Entities;

namespace ProductoService.Application.Abstractions.Persistence
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync(CancellationToken ct = default);
        Task<(IEnumerable<Producto>, int)> GetFilteredAsync(
            FiltrarProductoDto filtro,
            CancellationToken ct = default
        );
        Task<Producto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Producto?>> GetByIdsAsync(
            IEnumerable<Guid> ids,
            CancellationToken ct = default
        );
        Task<bool> ExistsAsync(string nombre, CancellationToken ct = default);
        Task<bool> ExistsAnotherAsync(Guid id, string nombre, CancellationToken ct = default);
        Task AddAsync(Producto entity, CancellationToken ct = default);
        Task UpdateAsync(Producto entity, CancellationToken ct = default);
        Task DeleteAsync(Producto entity, CancellationToken ct = default);
    }
}
