using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BonClaDetController : ControllerBase
    {
        private readonly IBonClaDetRepository _repository;
        private readonly ILogger<BonClaDetController> _logger;

        public BonClaDetController(
            IBonClaDetRepository repository,
            ILogger<BonClaDetController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/BonClaDet/GetAll
        /// Obtiene todos los detalles de bonificaciones (para sincronización)
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var detalles = await _repository.GetAllAsync();
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los detalles de bonificaciones");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// GET: api/BonClaDet/GetByCodClaBon/1
        /// Obtiene los escalones de una bonificación específica
        /// </summary>
        [HttpGet("GetByCodClaBon/{codClaBon}")]
        public async Task<IActionResult> GetByCodClaBon(short codClaBon)
        {
            try
            {
                var escalones = await _repository.GetByCodClaBonAsync(codClaBon);
                return Ok(escalones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obteniendo escalones de bonificación {codClaBon}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
