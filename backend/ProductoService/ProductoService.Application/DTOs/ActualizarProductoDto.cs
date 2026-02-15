namespace ProductoService.Application.DTOs
{
    public class ActualizarProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Categoria { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
    }
}
