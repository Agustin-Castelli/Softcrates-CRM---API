using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet("cliente/{codCli}")]
        public async Task<IActionResult> GetByClient(int codCli)
        {
            var facturas = await _facturaService.GetByClientAsync(codCli);
            return Ok(facturas);
        }
    }
}
