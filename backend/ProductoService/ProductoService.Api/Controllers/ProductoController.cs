using Microsoft.AspNetCore.Mvc;
using ProductoService.Application.DTOs;
using ProductoService.Application.Features.Commands;
using ProductoService.Application.Features.Queries;

namespace ProductoService.Api.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : Controller
    {
        private readonly ObtenerProductosHandler _obtenerHandler;
        private readonly ObtenerProductoPorIdHandler _obtenerPorIdHandler;
        private readonly ObtenerProductosPorIdsHandler _obtenerPorIdsHandler;
        private readonly CrearProductoHandler _crearHandler;
        private readonly ActualizarProductoHandler _actualizarHandler;
        private readonly EliminarProductoHandler _eliminarHandler;
        private readonly AumentarStockHandler _aumentarStockHandler;
        private readonly DisminuirStockHandler _disminuirStockHandler;

        public ProductoController(
            ObtenerProductosHandler obtenerHandler,
            ObtenerProductoPorIdHandler obtenerPorIdHandler,
            ObtenerProductosPorIdsHandler obtenerPorIdsHandler,
            CrearProductoHandler crearHandler,
            ActualizarProductoHandler actualizarHandler,
            EliminarProductoHandler eliminarHandler,
            AumentarStockHandler aumentarStockHandler,
            DisminuirStockHandler disminuirStockHandler
        )
        {
            _obtenerHandler = obtenerHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _obtenerPorIdsHandler = obtenerPorIdsHandler;
            _crearHandler = crearHandler;
            _actualizarHandler = actualizarHandler;
            _eliminarHandler = eliminarHandler;
            _aumentarStockHandler = aumentarStockHandler;
            _disminuirStockHandler = disminuirStockHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> ObtenerPorId(Guid id, CancellationToken ct)
        {
            var producto = await _obtenerPorIdHandler.Handle(id, ct);
            return Ok(producto);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<ProductoInfoDto>>> ObtenerPorIds(
            [FromBody] IEnumerable<Guid> ids,
            CancellationToken ct
        )
        {
            var result = await _obtenerPorIdsHandler.Handle(ids, ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(
            [FromQuery] FiltrarProductoDto filtro,
            CancellationToken ct
        )
        {
            var result = await _obtenerHandler.Handle(filtro, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearProductoDto dto, CancellationToken ct)
        {
            var id = await _crearHandler.Handle(dto, ct);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(
            Guid id,
            ActualizarProductoDto request,
            CancellationToken ct
        )
        {
            await _actualizarHandler.Handle(id, request, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id, CancellationToken ct)
        {
            await _eliminarHandler.Handle(id, ct);
            return NoContent();
        }

        [HttpPatch("{id}/aumentar-stock")]
        public async Task<IActionResult> AumentarStock(
            Guid id,
            [FromBody] int cantidad,
            CancellationToken ct
        )
        {
            await _aumentarStockHandler.Handle(id, cantidad, ct);
            return NoContent();
        }

        [HttpPatch("{id}/disminuir-stock")]
        public async Task<IActionResult> DisminuirStock(
            Guid id,
            [FromBody] int cantidad,
            CancellationToken ct
        )
        {
            await _disminuirStockHandler.Handle(id, cantidad, ct);
            return NoContent();
        }
    }
}
