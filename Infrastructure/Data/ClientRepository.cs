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
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<Client>();

            // Usamos EF.Functions.Like para control total del patrón SQL
            return await _context.Clientes
                .AsNoTracking()
                .Where(c => EF.Functions.Like(c.NomCli, $"%{name}%"))
                .OrderBy(c => c.NomCli)
                .ToListAsync();
        }
    }
}
