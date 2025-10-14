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
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ApplicationContext _context;

        public FacturaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> GetByClientAsync(int codCli)
        {
            return await _context.Facturas
                .AsNoTracking()
                .Where(f => f.CodCli == codCli)
                .OrderByDescending(f => f.FechaVto)
                .ToListAsync();
        }
    }
}
