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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationContext _context;

        public PedidoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PedWebCab> AgregarAsync(PedWebCab pedido)
        {
            _context.PedidosCab.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task ActualizarAsync(PedWebCab pedido)
        {
            _context.PedidosCab.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<PedWebArt> AgregarDetalleAsync(PedWebArt detalle)
        {
            _context.PedidosDet.Add(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<IEnumerable<PedWebCab>> ObtenerHistorialAsync(int codCli, int pageNumber, int pageSize)
        {
            return await _context.PedidosCab
                .Include(p => p.Detalles)
                .Where(p => p.CodCli == codCli)
                .OrderByDescending(p => p.FecCbt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<PedWebCab?> ObtenerPedidoAsync(string csid)
        {
            return await _context.PedidosCab
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Csid == csid);
        }

        public async Task<IEnumerable<PedWebCab>> GetAllPedidos()
        {
            return await _context.PedidosCab
                .Include(p => p.Detalles)
                .ToListAsync();
        }

        public async Task<int> ObtenerUltimoNroCbtAsync(short codTipCbt, short cemCbt)
        {
            return await _context.PedidosCab
                //.Where(p => p.CodTipCbt == codTipCbt && p.CemCbt == cemCbt) 
                .MaxAsync(p => (int?)p.NroCbt) ?? 0;
        }
    }

}
