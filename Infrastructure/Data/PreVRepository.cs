using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PreVRepository : IPreVRepository
    {
        private readonly ApplicationContext _context;

        public PreVRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<decimal?> ObtenerPrecioAsync(string codArt)
        {
            var precio = await _context.PrecioVenta
                .Where(p => p.CodArt == codArt)
                .Select(p => p.Precio)
                .FirstOrDefaultAsync();

            return precio == 0 ? null : precio;
        }
    }

}
