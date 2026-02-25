using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(
            IUserRepository userRepository,
            IHashingService hashingService,
            IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
            _options = options.Value;
        }

        public async Task<(User? user, string? token)> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return (null, null);

            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || user.Inactivo)
                return (null, null);

            // TRIM para manejar espacios de CHAR
            string storedPassword = user.PwdUsr?.Trim() ?? string.Empty;

            // Detectar si la contraseña está hasheada (BCrypt comienza con "$2")
            bool isPasswordHashed = storedPassword.StartsWith("$2");

            bool isValidPassword = false;

            if (isPasswordHashed)
            {
                // Contraseña ya hasheada - verificar con BCrypt
                isValidPassword = _hashingService.Verify(password, storedPassword);
            }
            else
            {
                // Contraseña en texto plano - comparar directamente
                if (storedPassword == password)
                {
                    // Login exitoso - hashear y actualizar
                    user.PwdUsr = _hashingService.Hash(password);
                    await _userRepository.UpdateAsync(user);
                    isValidPassword = true;
                }
            }

            if (!isValidPassword)
                return (null, null);

            // Generar JWT
            string token = GenerateJwtToken(user);

            return (user, token);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", user.CodUsr),
                new Claim("given_name", user.NomUsr),
                new Claim(ClaimTypes.Role, user.AdmUsr == "S" ? "Admin" : "User")
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(8), // Token válido por 8 horas
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "Authentication";

            public string Issuer { get; set; } = string.Empty;
            public string Audience { get; set; } = string.Empty;
            public string SecretForKey { get; set; } = string.Empty;
        }
    }
}
