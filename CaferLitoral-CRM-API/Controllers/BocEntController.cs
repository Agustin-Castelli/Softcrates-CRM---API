using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BocEntController : ControllerBase
    {
        private readonly IBocEntService _service;
        private readonly ILogger<BocEntController> _logger;

        public BocEntController(IBocEntService service, ILogger<BocEntController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/BocEnt/GetAll
        // Para sincronización inicial de la app móvil
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var bocas = await _service.GetAllAsync();
                return Ok(bocas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las bocas de entrega");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/BocEnt/GetByCliente/123
        // Lo que usa el frontend al seleccionar un cliente
        [HttpGet("[action]/{codCli}")]
        public async Task<IActionResult> GetByCliente(int codCli)
        {
            try
            {
                var bocas = await _service.GetByClienteAsync(codCli);
                return Ok(bocas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bocas del cliente {CodCli}", codCli);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/BocEnt/123/5
        // Para validaciones específicas
        [HttpGet("[action]/{codCli}/{nroBocEnt}")]
        public async Task<IActionResult> GetById(int codCli, int nroBocEnt)
        {
            try
            {
                var boca = await _service.GetByIdAsync(codCli, nroBocEnt);

                if (boca == null)
                    return NotFound($"No se encontró la boca {nroBocEnt} para el cliente {codCli}");

                return Ok(boca);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener boca {NroBocEnt} del cliente {CodCli}", nroBocEnt, codCli);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
