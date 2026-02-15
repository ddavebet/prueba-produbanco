namespace ProductoService.Application.DTOs
{
    public class FiltrarProductoDto
    {
        public string? Nombre { get; set; }
        public int? Categoria { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public int Pagina { get; set; } = 1;
        public int Tamano { get; set; } = 10;
    }
}
