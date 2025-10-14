using Application.Interfaces;
using Application.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// Crear un nuevo pedido con sus detalles
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPedido([FromBody] CrearPedidoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pedido = await _pedidoService.CrearPedidoAsync(dto);
            return Ok(pedido);
        }

        // Sincronizar los pedidos creados en modo offline (almacenados en SQLite)
        [HttpPost("[action]")]
        public async Task<IActionResult> SyncPedidos([FromBody] List<CrearPedidoDto> pedidosDto)
        {
            if (pedidosDto == null || !pedidosDto.Any())
                return BadRequest("No se recibieron pedidos para sincronizar.");
            var pedidosCreados = await _pedidoService.SyncPedidosCreadosAsync(pedidosDto);

            return Ok(pedidosCreados);
        }

        /// Obtener historial de pedidos por cliente
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerHistorial([FromQuery] int codCli, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var pedidos = await _pedidoService.ObtenerHistorialAsync(codCli, pageNumber, pageSize);
            return Ok(pedidos);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPedidos()
        {
            var pedidos = await _pedidoService.GetAllPedidos();
            return Ok(pedidos);
        }

        /// Obtener pedido por número
        [HttpGet("[action]/{csid}")]
        public async Task<IActionResult> ObtenerPedidoPorNumero(string csid)
        {
            var pedido = await _pedidoService.ObtenerPedidoAsync(csid);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }
    }
}
