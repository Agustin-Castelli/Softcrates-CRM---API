using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BonificacionController : ControllerBase
    {
        private readonly IBonificacionService _bonificacionService;
        private readonly ILogger<BonificacionController> _logger;

        public BonificacionController(
            IBonificacionService bonificacionService,
            ILogger<BonificacionController> logger)
        {
            _bonificacionService = bonificacionService;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/Bonificacion/GetArticulosConBonificacion/123
        /// Obtiene todos los artículos con sus bonificaciones para un cliente específico
        /// </summary>
        [HttpGet("GetArticulosConBonificacion/{codCli}")]
        public async Task<IActionResult> GetArticulosConBonificacion(int codCli)
        {
            try
            {
                var articulos = await _bonificacionService.ObtenerArticulosConBonificacionAsync(codCli);
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obteniendo artículos con bonificación para cliente {codCli}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// GET: api/Bonificacion/GetPorcentaje/123/ART001?cantidad=10&precioUnitario=100
        /// Calcula el porcentaje de bonificación para un artículo según cantidad e importe
        /// </summary>
        [HttpGet("GetPorcentaje/{codCli}/{codArt}")]
        public async Task<IActionResult> GetPorcentaje(
            int codCli,
            string codArt,
            [FromQuery] decimal cantidad,
            [FromQuery] decimal precioUnitario)
        {
            try
            {
                var porcentaje = await _bonificacionService.ObtenerPorcentajeBonificacionAsync(
                    codCli, codArt, cantidad, precioUnitario);

                return Ok(new { CodCli = codCli, CodArt = codArt, Porcentaje = porcentaje });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calculando bonificación para cliente {codCli} y artículo {codArt}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// GET: api/Bonificacion/GetBonificacionBase/123/ART001
        /// Obtiene la bonificación base (primer escalón) para mostrar en listados
        /// </summary>
        [HttpGet("GetBonificacionBase/{codCli}/{codArt}")]
        public async Task<IActionResult> GetBonificacionBase(int codCli, string codArt)
        {
            try
            {
                var bonificacion = await _bonificacionService.ObtenerBonificacionBaseAsync(codCli, codArt);

                if (bonificacion == null)
                    return NotFound($"No hay bonificación para cliente {codCli} y artículo {codArt}");

                return Ok(bonificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obteniendo bonificación base para cliente {codCli} y artículo {codArt}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
