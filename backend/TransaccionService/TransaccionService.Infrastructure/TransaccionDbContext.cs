using Microsoft.EntityFrameworkCore;
using TransaccionService.Domain.Entities;

namespace TransaccionService.Infrastructure
{
    public class TransaccionDbContext : DbContext
    {
        public TransaccionDbContext(DbContextOptions<TransaccionDbContext> options)
            : base(options) { }

        public virtual DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransaccionDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
