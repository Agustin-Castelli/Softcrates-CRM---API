using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaferLitoral_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous] // ⬅️ Permite acceso sin token
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Usuario o contraseña inválidos.");

            var (user, token) = await _authenticationService.AuthenticateAsync(request.Username, request.Password);

            if (user == null || token == null)
                return Unauthorized(new UserLoginResponse(
                    codUsr: string.Empty,
                    name: string.Empty,
                    isAdmin: string.Empty,
                    token: string.Empty
                ));

            var response = new UserLoginResponse(
                codUsr: user.CodUsr,
                name: user.NomUsr,
                isAdmin: user.AdmUsr,
                token: token // ⬅️ AHORA SÍ retorna el token
            );

            return Ok(response);
        }
    }
}