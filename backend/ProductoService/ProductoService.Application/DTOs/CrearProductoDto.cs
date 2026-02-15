namespace ProductoService.Application.DTOs
{
    public class CrearProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Categoria { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public int StockInicial { get; set; }
    }
}
