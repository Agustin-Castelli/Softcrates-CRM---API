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
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly ApplicationContext _context;

        public ArticuloRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artic>> GetAll() 
        {
            return await _context.Articulos.Where(a => a.Inactivo == 0).ToListAsync(); 
        }

        public async Task<IEnumerable<Artic>> ObtenerArticulosAsync(int pageNumber, int pageSize)
        {
            return await _context.Articulos
                .Where(a => a.Inactivo == 0)
                .OrderBy(a => a.DesArt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<IEnumerable<Artic>> BuscarPorNombreAsync(string nombre)
        {
            return await _context.Articulos
                .Where(a => a.Inactivo == 0 && a.DesArt.Contains(nombre))
                .ToListAsync();
        }

        public async Task<Artic?> ObtenerPorCodigoAsync(string codArt)
        {
            return await _context.Articulos
                .Where(a => a.Inactivo == 0)
                .FirstOrDefaultAsync(a => a.CodArt == codArt);
        }
    }

}
