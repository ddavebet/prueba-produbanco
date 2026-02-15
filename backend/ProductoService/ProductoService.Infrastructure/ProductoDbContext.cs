using Microsoft.EntityFrameworkCore;
using ProductoService.Domain.Entities;

namespace ProductoService.Infrastructure
{
    public class ProductoDbContext : DbContext
    {
        public ProductoDbContext(DbContextOptions<ProductoDbContext> options)
            : base(options) { }

        public virtual DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
