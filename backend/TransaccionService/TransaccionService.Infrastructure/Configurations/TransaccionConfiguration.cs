using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransaccionService.Domain.Entities;

namespace TransaccionService.Infrastructure.Configurations
{
    internal class TransaccionConfiguration : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
            builder.ToTable("Transacciones");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fecha).IsRequired();

            builder.Property(x => x.Tipo).HasConversion<int>().IsRequired();

            builder.Property(x => x.ProductoId).IsRequired();

            builder.Property(x => x.Cantidad).IsRequired();

            builder.Property(x => x.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.PrecioTotal).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.Detalle).HasMaxLength(500);
        }
    }
}
