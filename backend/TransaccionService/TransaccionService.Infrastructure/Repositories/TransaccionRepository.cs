using Microsoft.EntityFrameworkCore;
using TransaccionService.Application.Abstractions.Persistence;
using TransaccionService.Application.DTOs;
using TransaccionService.Domain.Entities;
using TransaccionService.Domain.Enums;

namespace TransaccionService.Infrastructure.Repositories
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly TransaccionDbContext _context;

        public TransaccionRepository(TransaccionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaccion>> GetAllAsync(CancellationToken ct = default)
        {
            var entity = await _context.Transacciones.AsNoTracking().ToListAsync(ct);
            return entity;
        }

        public async Task<(IEnumerable<Transaccion>, int)> GetFilteredAsync(
            FiltrarTransaccionDto filtro,
            CancellationToken ct = default
        )
        {
            IQueryable<Transaccion> query = _context.Transacciones.AsNoTracking().AsQueryable();

            if (filtro.FechaInicio.HasValue)
            {
                query = query.Where(t => t.Fecha >= filtro.FechaInicio.Value);
            }

            if (filtro.FechaFin.HasValue)
            {
                query = query.Where(t => t.Fecha <= filtro.FechaFin.Value);
            }

            if (filtro.Tipo.HasValue)
            {
                query = query.Where(t => t.Tipo == (TipoTransaccion)filtro.Tipo);
            }

            if (filtro.ProductoId.HasValue)
            {
                query = query.Where(t => t.ProductoId == filtro.ProductoId.Value);
            }

            var total = await query.CountAsync(ct);

            var transacciones = await query
                .OrderByDescending(p => p.Fecha)
                .Skip((filtro.Pagina - 1) * filtro.Tamano)
                .Take(filtro.Tamano)
                .ToListAsync(ct);

            return (transacciones, total);
        }

        public async Task<Transaccion?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Transacciones.FindAsync(id, ct);
            return entity;
        }

        public async Task AddAsync(Transaccion entity, CancellationToken ct = default)
        {
            _context.Transacciones.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Transaccion entity, CancellationToken ct = default)
        {
            _context.Transacciones.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Transaccion entity, CancellationToken ct = default)
        {
            _context.Transacciones.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
