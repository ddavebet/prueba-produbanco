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
        private readonly CrearProductoHandler _crearHandler;
        private readonly ActualizarProductoHandler _actualizarHandler;
        private readonly EliminarProductoHandler _eliminarHandler;

        public ProductoController(
            ObtenerProductosHandler obtenerHandler,
            ObtenerProductoPorIdHandler obtenerPorIdHandler,
            CrearProductoHandler crearHandler,
            ActualizarProductoHandler actualizarHandler,
            EliminarProductoHandler eliminarHandler
        )
        {
            _obtenerHandler = obtenerHandler;
            _obtenerPorIdHandler = obtenerPorIdHandler;
            _crearHandler = crearHandler;
            _actualizarHandler = actualizarHandler;
            _eliminarHandler = eliminarHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetById(Guid id, CancellationToken ct)
        {
            var producto = await _obtenerPorIdHandler.Handle(id, ct);
            return Ok(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] FiltrarProductoDto filtro,
            CancellationToken ct
        )
        {
            var result = await _obtenerHandler.Handle(filtro, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CrearProductoDto dto, CancellationToken ct)
        {
            var id = await _crearHandler.Handle(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            ActualizarProductoDto request,
            CancellationToken ct
        )
        {
            await _actualizarHandler.Handle(id, request, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _eliminarHandler.Handle(id, ct);
            return NoContent();
        }
    }
}
