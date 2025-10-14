using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Usuario o contraseña inválidos.");

            var user = await _userService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
                return Unauthorized(new UserLoginResponse(
                    codUsr: string.Empty,
                    name: string.Empty,
                    isAdmin: string.Empty,
                    token: string.Empty
                ));


            // Más adelante acá podríamos generar el JWT


            var response = new UserLoginResponse(
                codUsr: user.CodUsr,
                name: user.NomUsr,
                isAdmin: user.AdmUsr,
                token: string.Empty // Por ahora vacío
            );

            return Ok(response);
        }
    }
}
