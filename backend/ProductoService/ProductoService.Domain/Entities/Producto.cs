using ProductoService.Domain.Enums;
using ProductoService.Domain.Exceptions;

namespace ProductoService.Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public string? Descripcion { get; private set; }
        public Categoria Categoria { get; private set; }
        public string? Imagen { get; private set; }
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }

        private Producto() { }

        public Producto(
            string nombre,
            string descripcion,
            Categoria categoria,
            string imagen,
            decimal precio,
            int stock
        )
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new DomainException("El nombre no puede estar vacío.");
            }

            if (precio < 0)
            {
                throw new DomainException("El precio no puede ser negativo.");
            }

            if (stock < 0)
            {
                throw new DomainException("El stock no puede ser negativo.");
            }

            Id = Guid.NewGuid();
            Nombre = nombre;
            Descripcion = descripcion;
            Categoria = categoria;
            Imagen = imagen;
            Precio = precio;
            Stock = stock;
        }

        public void Actualizar(
            string nombre,
            string descripcion,
            Categoria categoria,
            string imagen,
            decimal precio
        )
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new DomainException("El nombre no puede estar vacío.");
            }
            if (precio < 0)
            {
                throw new DomainException("El precio no puede ser negativo.");
            }
            Nombre = nombre;
            Descripcion = descripcion;
            Categoria = categoria;
            Imagen = imagen;
            Precio = precio;
        }

        public void AumentarStock(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new DomainException("La cantidad debe ser mayor que cero.");
            }

            Stock += cantidad;
        }

        public void DisminuirStock(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new DomainException("La cantidad debe ser mayor que cero.");
            }

            if (Stock < cantidad)
            {
                throw new DomainException("No hay stock suficiente.");
            }

            Stock -= cantidad;
        }
    }
}
