using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var articulos = await _articuloService.GetAll();
            return Ok(articulos);
        }

        /// Obtener todos los artículos PAGINADOS
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerTodos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var articulos = await _articuloService.ObtenerArticulosAsync(pageNumber, pageSize);
            return Ok(articulos);
        }

        [HttpGet("[action]/{nombre}")]
        public async Task<IActionResult> BuscarPorNombre(string nombre)
        {
            var articulos = await _articuloService.BuscarPorNombreAsync(nombre);

            return Ok(articulos);
        }

        /// <summary>
        /// Obtener artículo por código
        /// </summary>
        [HttpGet("[action]/{codArt}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codArt)
        {
            var articulo = await _articuloService.ObtenerPorCodigoAsync(codArt);
            if (articulo == null)
                return NotFound();

            return Ok(articulo);
        }
    }
}
