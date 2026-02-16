using TransaccionService.Application.Abstractions.Persistence;
using TransaccionService.Application.Abstractions.Services;
using TransaccionService.Application.DTOs;
using TransaccionService.Domain.Entities;
using TransaccionService.Domain.Enums;

namespace TransaccionService.Application.Features.Commands
{
    public class CrearTransaccionHandler
    {
        private readonly ITransaccionRepository _repository;
        private readonly IProductoClient _productoClient;

        public CrearTransaccionHandler(
            ITransaccionRepository repository,
            IProductoClient productoClient
        )
        {
            _repository = repository;
            _productoClient = productoClient;
        }

        public async Task<Guid> Handle(CrearTransaccionDto dto, CancellationToken ct = default)
        {
            var producto = await _productoClient.ObtenerProductoPorIdAsync(dto.ProductoId);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            if (dto.Tipo == (int)TipoTransaccion.Venta)
            {
                await _productoClient.DisminuirStockAsync(dto.ProductoId, dto.Cantidad);
            }
            else
            {
                await _productoClient.AumentarStockAsync(dto.ProductoId, dto.Cantidad);
            }

            var transaction = new Transaccion(
                (TipoTransaccion)dto.Tipo,
                dto.ProductoId,
                dto.Cantidad,
                producto.Precio,
                dto.Detalle
            );

            await _repository.AddAsync(transaction, ct);

            return transaction.Id;
        }
    }
}
