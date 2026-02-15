using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Application.DTOs;

namespace ProductoService.Application.Features.Queries
{
    public class ObtenerProductoPorIdHandler
    {
        private readonly IProductoRepository _repository;

        public ObtenerProductoPorIdHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductoDto> Handle(Guid id, CancellationToken ct)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            var response = new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Categoria = producto.Categoria.ToString(),
                Imagen = producto.Imagen,
                Precio = producto.Precio,
                Stock = producto.Stock,
            };
            return response;
        }
    }
}
