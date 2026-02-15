using TransaccionService.Application.DTOs;
using TransaccionService.Domain.Entities;

namespace TransaccionService.Application.Abstractions.Persistence
{
    public interface ITransaccionRepository
    {
        Task<IEnumerable<Transaccion>> GetAllAsync(CancellationToken ct = default);
        Task<(IEnumerable<Transaccion>, int)> GetFilteredAsync(
            FiltrarTransaccionDto filtro,
            CancellationToken ct = default
        );
        Task<Transaccion?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Transaccion entity, CancellationToken ct = default);
        Task UpdateAsync(Transaccion entity, CancellationToken ct = default);
        Task DeleteAsync(Transaccion entity, CancellationToken ct = default);
    }
}
