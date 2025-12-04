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
    public class BonArtCliRepository : IBonArtCliRepository
    {
        private readonly ApplicationContext _context;

        public BonArtCliRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BonArtCli>> GetAllAsync()
        {
            return await _context.BonificacionesArticuloCliente
                .Where(b => !b.Inactivo)
                .ToListAsync();
        }

        public async Task<IEnumerable<BonArtCli>> GetByClienteAsync(int codCli)
        {
            return await _context.BonificacionesArticuloCliente
                .Where(b => b.CodCli == codCli && !b.Inactivo)
                .ToListAsync();
        }

        public async Task<BonArtCli?> GetByClienteYArticuloAsync(int codCli, string codArt)
        {
            return await _context.BonificacionesArticuloCliente
                .FirstOrDefaultAsync(b => b.CodCli == codCli && b.CodArt == codArt && !b.Inactivo);
        }
    }
}

