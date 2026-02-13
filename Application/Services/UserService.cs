using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;

        public UserService(IUserRepository userRepository, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
        }


        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || user.Inactivo)
                return null;

            // ⬅️ TRIM para manejar espacios de CHAR
            string storedPassword = user.PwdUsr?.Trim() ?? string.Empty;

            // Detectar si la contraseña está hasheada (BCrypt comienza con "$2")
            bool isPasswordHashed = storedPassword.StartsWith("$2");

            if (isPasswordHashed)
            {
                // ✅ Contraseña ya hasheada - verificar con BCrypt
                if (_hashingService.Verify(password, storedPassword))
                {
                    return user;
                }
            }
            else
            {
                // ⚠️ Contraseña en texto plano - comparar directamente
                if (storedPassword == password) // ⬅️ Ahora compara sin espacios
                {
                    // ✅ Login exitoso - AHORA hashear y actualizar
                    user.PwdUsr = _hashingService.Hash(password);
                    await _userRepository.UpdateAsync(user);

                    return user;
                }
            }

            return null;
        }

        //public async Task<User?> AuthenticateAsync(string username, string password)
        //{
        //    // Aquí más adelante podés meter validación de hash y JWT
        //    return await _userRepository.GetByCredentialsAsync(username, password);
        //}
    }
}
