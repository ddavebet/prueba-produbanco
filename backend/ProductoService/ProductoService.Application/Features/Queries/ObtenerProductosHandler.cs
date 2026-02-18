using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Application.DTOs;
using ProductoService.Domain.Entities;

namespace ProductoService.Application.Features.Queries
{
    public class ObtenerProductosHandler
    {
        private readonly IProductoRepository _repository;

        public ObtenerProductosHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductoResultadoDto> Handle(
            FiltrarProductoDto filtro,
            CancellationToken ct
        )
        {
            var (productos, total) = await _repository.GetFilteredAsync(filtro, ct);

            var response = new ProductoResultadoDto
            {
                Productos = productos.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    CategoriaId = (int)p.Categoria,
                    Categoria = p.Categoria.ToString(),
                    Imagen = p.Imagen,
                    Precio = p.Precio,
                    Stock = p.Stock,
                }),
                Total = total,
                Pagina = filtro.Pagina,
                Tamano = filtro.Tamano,
            };
            return response;
        }
    }
}
