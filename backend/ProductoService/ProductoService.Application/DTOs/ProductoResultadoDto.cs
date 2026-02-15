namespace ProductoService.Application.DTOs
{
    public class ProductoResultadoDto
    {
        public IEnumerable<ProductoDto> Productos { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int Tamano { get; set; }
    }
}
