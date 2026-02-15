namespace TransaccionService.Application.DTOs
{
    public class TransaccionResultadoDto
    {
        public IEnumerable<TransaccionDto> Transacciones { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int Tamano { get; set; }
    }
}
