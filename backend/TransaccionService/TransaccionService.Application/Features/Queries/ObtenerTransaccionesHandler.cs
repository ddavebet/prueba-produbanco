using TransaccionService.Application.Abstractions.Persistence;
using TransaccionService.Application.DTOs;

namespace TransaccionService.Application.Features.Queries
{
    public class ObtenerTransaccionesHandler
    {
        private readonly ITransaccionRepository _repository;

        public ObtenerTransaccionesHandler(ITransaccionRepository repository)
        {
            _repository = repository;
        }

        public async Task<TransaccionResultadoDto> Handle(
            FiltrarTransaccionDto filtro,
            CancellationToken ct
        )
        {
            var (transacciones, total) = await _repository.GetFilteredAsync(filtro, ct);

            var response = new TransaccionResultadoDto
            {
                Transacciones = transacciones.Select(p => new TransaccionDto
                {
                    Id = p.Id,
                    Fecha = p.Fecha,
                    Tipo = p.Tipo.ToString(),
                    ProductoId = p.ProductoId,
                    //ProductoNombre = p.ProductoNombre,
                    //ProductoStock = p.ProductoStock,
                    Cantidad = p.Cantidad,
                    PrecioUnitario = p.PrecioUnitario,
                    PrecioTotal = p.PrecioTotal,
                    Detalle = p.Detalle,
                }),
                Total = total,
                Pagina = filtro.Pagina,
                Tamano = filtro.Tamano,
            };
            return response;
        }
    }
}
