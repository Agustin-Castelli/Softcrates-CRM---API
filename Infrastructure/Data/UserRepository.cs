using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NomUsr == username);
        }

        // Agregar método para actualizar
        public async Task UpdateAsync(User user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
        }


        //public async Task<User?> GetByCredentialsAsync(string username, string password)
        //{
        //    // Nota: Esto no encripta ni valida hash, solo compara texto plano
        //    return await _context.Usuarios
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(u => u.NomUsr == username && u.PwdUsr == password && !u.Inactivo);
        //}
    }
}
