namespace ProductoService.Application.DTOs
{
    public class ProductoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
