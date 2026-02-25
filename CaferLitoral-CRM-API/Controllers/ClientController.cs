using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("[action]/{codCli}")]
        public async Task<IActionResult> GetById(int codCli)
        {
            var client = await _clientService.GetByIdAsync(codCli);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var clients = await _clientService.SearchByNameAsync(name);
            return Ok(clients);
        }

        // Nuevo endpoint optimizado
        [HttpGet("resumen/{codCli}")]
        public async Task<IActionResult> GetResumen(int codCli)
        {
            var resumen = await _clientService.GetResumenAsync(codCli);
            if (resumen == null)
                return NotFound();

            return Ok(resumen);
        }
    }
}
