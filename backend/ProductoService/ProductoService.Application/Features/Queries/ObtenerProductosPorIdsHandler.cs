using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Application.DTOs;

namespace ProductoService.Application.Features.Queries
{
    public class ObtenerProductosPorIdsHandler
    {
        private readonly IProductoRepository _repository;

        public ObtenerProductosPorIdsHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductoInfoDto>> Handle(
            IEnumerable<Guid> ids,
            CancellationToken ct
        )
        {
            var productos = await _repository.GetByIdsAsync(ids);

            var response = productos.Select(p => new ProductoInfoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Stock = p.Stock,
            });
            return response;
        }
    }
}
