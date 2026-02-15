namespace TransaccionService.Application.DTOs
{
    public class FiltrarTransaccionDto
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? Tipo { get; set; }
        public Guid? ProductoId { get; set; }
        public int Pagina { get; set; } = 1;
        public int Tamano { get; set; } = 10;
    }
}
