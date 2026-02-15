using TransaccionService.Domain.Enums;
using TransaccionService.Domain.Exceptions;

namespace TransaccionService.Domain.Entities
{
    public class Transaccion
    {
        public Guid Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public TipoTransaccion Tipo { get; private set; }
        public Guid ProductoId { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public decimal PrecioTotal { get; private set; }
        public string? Detalle { get; private set; }

        private Transaccion() { }

        public Transaccion(
            TipoTransaccion tipo,
            Guid productoId,
            int cantidad,
            decimal precioUnitario,
            string? detalle
        )
        {
            if (productoId == Guid.Empty)
            {
                throw new DomainException("El producto es obligatorio.");
            }

            if (cantidad <= 0)
            {
                throw new DomainException("La cantidad debe ser mayor que cero.");
            }

            if (precioUnitario <= 0)
            {
                throw new DomainException("El precio unitario debe ser mayor que cero.");
            }

            Id = Guid.NewGuid();
            Fecha = DateTime.UtcNow;
            Tipo = tipo;
            ProductoId = productoId;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            PrecioTotal = CalcularTotal(cantidad, precioUnitario);
            Detalle = detalle;
        }

        private decimal CalcularTotal(int cantidad, decimal precioUnitario)
        {
            return cantidad * precioUnitario;
        }
    }
}
