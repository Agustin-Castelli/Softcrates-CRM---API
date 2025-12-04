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
    public class BocEntRepository : IBocEntRepository
    {
        private readonly ApplicationContext _context;

        public BocEntRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Obtener TODAS las bocas (para sincronización inicial en la app)
        public async Task<IEnumerable<BocEnt>> GetAllAsync()
        {
            return await _context.BocaEntrega
                .OrderBy(b => b.CodCli)
                .ThenBy(b => b.NroBocEnt)
                .ToListAsync();
        }

        // Obtener bocas de UN cliente específico (lo que necesita el frontend)
        public async Task<IEnumerable<BocEnt>> GetByClienteAsync(int codCli)
        {
            return await _context.BocaEntrega
                .Where(b => b.CodCli == codCli)
                .OrderBy(b => b.NroBocEnt)
                .ToListAsync();
        }

        // Obtener UNA boca específica (para validaciones)
        public async Task<BocEnt?> GetByIdAsync(int codCli, int nroBocEnt)
        {
            return await _context.BocaEntrega
                .FirstOrDefaultAsync(b => b.CodCli == codCli && b.NroBocEnt == nroBocEnt);
        }

        // CRUD adicionales (por si necesitás administrar bocas desde el sistema)
        public async Task<BocEnt> AddAsync(BocEnt bocaEntrega)
        {
            _context.BocaEntrega.Add(bocaEntrega);
            await _context.SaveChangesAsync();
            return bocaEntrega;
        }

        public async Task UpdateAsync(BocEnt bocaEntrega)
        {
            _context.BocaEntrega.Update(bocaEntrega);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int codCli, int nroBocEnt)
        {
            var boca = await GetByIdAsync(codCli, nroBocEnt);
            if (boca != null)
            {
                _context.BocaEntrega.Remove(boca);
                await _context.SaveChangesAsync();
            }
        }
    }
}
