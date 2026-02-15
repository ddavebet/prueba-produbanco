using TransaccionService.Application.Abstractions.Persistence;
using TransaccionService.Application.Abstractions.Services;
using TransaccionService.Application.DTOs;
using static System.Net.WebRequestMethods;

namespace TransaccionService.Application.Features.Queries
{
    public class ObtenerTransaccionesHandler
    {
        private readonly ITransaccionRepository _repository;
        private readonly IProductoClient _productoClient;

        public ObtenerTransaccionesHandler(
            ITransaccionRepository repository,
            IProductoClient productoClient
        )
        {
            _repository = repository;
            _productoClient = productoClient;
        }

        public async Task<TransaccionResultadoDto> Handle(
            FiltrarTransaccionDto filtro,
            CancellationToken ct
        )
        {
            var (transacciones, total) = await _repository.GetFilteredAsync(filtro, ct);

            var productoIds = transacciones.Select(t => t.ProductoId).Distinct();

            var products = await _productoClient.ObtenerProductosPorIdsAsync(productoIds);

            var transaccionesConProductoInfo = (
                from t in transacciones
                join p in products on t.ProductoId equals p.Id into tp
                from p in tp.DefaultIfEmpty()
                select new TransaccionDto
                {
                    Id = t.Id,
                    Fecha = t.Fecha,
                    Tipo = t.Tipo.ToString(),
                    ProductoId = t.ProductoId,
                    ProductoNombre = p?.Nombre ?? "Desconocido",
                    ProductoStock = p?.Stock ?? 0,
                    Cantidad = t.Cantidad,
                    PrecioUnitario = t.PrecioUnitario,
                    PrecioTotal = t.PrecioTotal,
                    Detalle = t.Detalle,
                }
            );

            var response = new TransaccionResultadoDto
            {
                Transacciones = transaccionesConProductoInfo,
                Total = total,
                Pagina = filtro.Pagina,
                Tamano = filtro.Tamano,
            };
            return response;
        }
    }
}
