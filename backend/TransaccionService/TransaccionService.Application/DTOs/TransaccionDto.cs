namespace TransaccionService.Application.DTOs
{
    public class TransaccionDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public Guid ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int ProductoStock { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public string? Detalle { get; set; }
    }
}
