using Microsoft.AspNetCore.Mvc;
using TransaccionService.Application.DTOs;
using TransaccionService.Application.Features.Commands;
using TransaccionService.Application.Features.Queries;

namespace TransaccionService.Api.Controllers
{
    [ApiController]
    [Route("api/transacciones")]
    public class TransaccionController : Controller
    {
        private readonly ObtenerTransaccionesHandler _obtenerHandler;
        private readonly CrearTransaccionHandler _crearHandler;

        public TransaccionController(
            ObtenerTransaccionesHandler obtenerHandler,
            CrearTransaccionHandler crearHandler
        )
        {
            _obtenerHandler = obtenerHandler;
            _crearHandler = crearHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(
            [FromQuery] FiltrarTransaccionDto filtro,
            CancellationToken ct
        )
        {
            var result = await _obtenerHandler.Handle(filtro, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearTransaccionDto dto, CancellationToken ct)
        {
            var id = await _crearHandler.Handle(dto, ct);
            return Created();
        }
    }
}
