using Microsoft.EntityFrameworkCore;
using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Domain.Entities;

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
            var entity = await _context.Productos.ToListAsync(ct);
            return entity;
        }

        public async Task<Producto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Productos.FindAsync(id);
            return entity;
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
    }
}
