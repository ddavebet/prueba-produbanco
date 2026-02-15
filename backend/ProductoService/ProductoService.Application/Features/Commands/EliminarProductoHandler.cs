using ProductoService.Application.Abstractions.Persistence;

namespace ProductoService.Application.Features.Commands
{
    public class EliminarProductoHandler
    {
        private readonly IProductoRepository _repository;

        public EliminarProductoHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Guid id, CancellationToken ct = default)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            await _repository.DeleteAsync(producto, ct);
        }
    }
}
