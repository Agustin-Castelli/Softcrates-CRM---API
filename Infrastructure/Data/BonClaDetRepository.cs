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
    public class BonClaDetRepository : IBonClaDetRepository
    {
        private readonly ApplicationContext _context;

        public BonClaDetRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BonClaDet>> GetAllAsync()
        {
            return await _context.BonificacionesClaseDetalle
                .OrderBy(b => b.CodClaBon)
                .ThenBy(b => b.Secuencia)
                .ToListAsync();
        }

        public async Task<IEnumerable<BonClaDet>> GetByCodClaBonAsync(short codClaBon)
        {
            return await _context.BonificacionesClaseDetalle
                .Where(b => b.CodClaBon == codClaBon)
                .OrderBy(b => b.Secuencia)
                .ToListAsync();
        }

        public async Task<BonClaDet?> GetByIdAsync(short codClaBon, short secuencia)
        {
            return await _context.BonificacionesClaseDetalle
                .FirstOrDefaultAsync(b => b.CodClaBon == codClaBon && b.Secuencia == secuencia);
        }
    }
}
