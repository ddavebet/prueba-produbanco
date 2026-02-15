using ProductoService.Application.Abstractions.Persistence;
using ProductoService.Application.DTOs;
using ProductoService.Domain.Enums;

namespace ProductoService.Application.Features.Commands
{
    public class ActualizarProductoHandler
    {
        private readonly IProductoRepository _repository;

        public ActualizarProductoHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Guid id, ActualizarProductoDto dto, CancellationToken ct = default)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            var existe = await _repository.ExistsAnotherAsync(id, dto.Nombre, ct);
            if (existe)
            {
                throw new InvalidOperationException("Ya existe un producto con el mismo nombre.");
            }

            producto.Actualizar(
                dto.Nombre,
                dto.Descripcion,
                (Categoria)dto.Categoria,
                dto.Imagen,
                dto.Precio
            );

            await _repository.UpdateAsync(producto, ct);
        }
    }
}
