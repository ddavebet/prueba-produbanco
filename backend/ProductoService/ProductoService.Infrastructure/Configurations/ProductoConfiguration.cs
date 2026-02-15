using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductoService.Domain.Entities;

namespace ProductoService.Infrastructure.Configurations
{
    internal class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Descripcion).HasMaxLength(200);

            builder.Property(x => x.Categoria).HasConversion<int>().IsRequired();

            builder.Property(x => x.Imagen).HasMaxLength(500);

            builder.Property(x => x.Precio).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.Stock).IsRequired();
        }
    }
}
