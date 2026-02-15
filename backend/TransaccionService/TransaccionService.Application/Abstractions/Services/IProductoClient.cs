namespace TransaccionService.Application.Abstractions.Services
{
    public interface IProductoClient
    {
        Task<bool> ExisteProductoAsync(Guid productoId);
        Task AumentarStockAsync(Guid productoId, int cantidad);
        Task DisminuirStockAsync(Guid productoId, int cantidad);
    }
}
