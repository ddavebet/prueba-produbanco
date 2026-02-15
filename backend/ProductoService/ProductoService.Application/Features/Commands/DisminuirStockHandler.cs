using ProductoService.Application.Abstractions.Persistence;

namespace ProductoService.Application.Features.Commands
{
    public class DisminuirStockHandler
    {
        private readonly IProductoRepository _repository;

        public DisminuirStockHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Guid id, int cantidad, CancellationToken ct = default)
        {
            var producto = await _repository.GetByIdAsync(id, ct);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            producto.DisminuirStock(cantidad);

            await _repository.UpdateAsync(producto, ct);
        }
    }
}
