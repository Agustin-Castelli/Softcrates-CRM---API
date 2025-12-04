using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonArtCliController : ControllerBase
    {
        private readonly IBonArtCliRepository _repository;
        private readonly ILogger<BonArtCliController> _logger;

        public BonArtCliController(
            IBonArtCliRepository repository,
            ILogger<BonArtCliController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/BonArtCli/GetAll
        /// Obtiene todas las asignaciones de bonificaciones (para sincronización)
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var bonificaciones = await _repository.GetAllAsync();
                return Ok(bonificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todas las bonificaciones de artículos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// GET: api/BonArtCli/GetByCliente/123
        /// Obtiene las bonificaciones de un cliente específico
        /// </summary>
        [HttpGet("GetByCliente/{codCli}")]
        public async Task<IActionResult> GetByCliente(int codCli)
        {
            try
            {
                var bonificaciones = await _repository.GetByClienteAsync(codCli);
                return Ok(bonificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obteniendo bonificaciones del cliente {codCli}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
