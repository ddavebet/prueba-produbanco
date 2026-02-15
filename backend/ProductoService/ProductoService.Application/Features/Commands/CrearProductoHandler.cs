using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Application.DTOs;
using ProductoService.Domain.Entities;
using ProductoService.Domain.Enums;

namespace ProductoService.Application.Features.Commands
{
    public class CrearProductoHandler
    {
        private readonly IProductoRepository _repository;

        public CrearProductoHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CrearProductoDto dto, CancellationToken ct = default)
        {
            var existe = await _repository.ExistsAsync(dto.Nombre, ct);
            if (existe)
            {
                throw new InvalidOperationException("Ya existe un producto con el mismo nombre.");
            }

            var producto = new Producto(
                dto.Nombre,
                dto.Descripcion,
                (Categoria)dto.Categoria,
                dto.Imagen,
                dto.Precio,
                dto.StockInicial
            );

            await _repository.AddAsync(producto, ct);

            return producto.Id;
        }
    }
}
