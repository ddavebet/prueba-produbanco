using Microsoft.EntityFrameworkCore;
using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Application.DTOs;
using ProductoService.Domain.Entities;
using ProductoService.Domain.Enums;
using static System.Net.WebRequestMethods;

namespace ProductoService.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ProductoDbContext _context;

        public ProductoRepository(ProductoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync(CancellationToken ct = default)
        {
            var entity = await _context.Productos.AsNoTracking().ToListAsync(ct);
            return entity;
        }

        public async Task<(IEnumerable<Producto>, int)> GetFilteredAsync(
            FiltrarProductoDto filtro,
            CancellationToken ct = default
        )
        {
            IQueryable<Producto> query = _context.Productos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                query = query.Where(p => p.Nombre.Contains(filtro.Nombre));
            }

            if (filtro.Categoria.HasValue)
            {
                query = query.Where(p => p.Categoria == (Categoria)filtro.Categoria);
            }

            if (filtro.PrecioMin.HasValue)
            {
                query = query.Where(p => p.Precio >= filtro.PrecioMin.Value);
            }

            if (filtro.PrecioMax.HasValue)
            {
                query = query.Where(p => p.Precio <= filtro.PrecioMax.Value);
            }

            var total = await query.CountAsync(ct);

            var products = await query
                .OrderBy(p => p.Nombre)
                .Skip((filtro.Pagina - 1) * filtro.Tamano)
                .Take(filtro.Tamano)
                .ToListAsync(ct);

            return (products, total);
        }

        public async Task<Producto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Productos.FindAsync(id, ct);
            return entity;
        }

        public async Task<bool> ExistsAsync(string nombre, CancellationToken ct = default)
        {
            var exists = await _context.Productos.AnyAsync(p => p.Nombre == nombre, ct);
            return exists;
        }

        public async Task<bool> ExistsAnotherAsync(
            Guid id,
            string nombre,
            CancellationToken ct = default
        )
        {
            var exists = await _context.Productos.AnyAsync(
                p => p.Nombre == nombre && p.Id != id,
                ct
            );
            return exists;
        }

        public async Task AddAsync(Producto entity, CancellationToken ct = default)
        {
            _context.Productos.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Producto entity, CancellationToken ct = default)
        {
            _context.Productos.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Producto entity, CancellationToken ct = default)
        {
            _context.Productos.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
