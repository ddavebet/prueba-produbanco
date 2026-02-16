namespace TransaccionService.Application.DTOs
{
    public class CrearTransaccionDto
    {
        public int Tipo { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string? Detalle { get; set; }
    }
}
