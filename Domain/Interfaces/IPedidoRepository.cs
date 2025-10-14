using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<PedWebCab> AgregarAsync(PedWebCab pedido);
        Task ActualizarAsync(PedWebCab pedido);
        Task<PedWebArt> AgregarDetalleAsync(PedWebArt detalle);

        Task<IEnumerable<PedWebCab>> ObtenerHistorialAsync(int codCli, int pageNumber, int pageSize);

        Task<PedWebCab?> ObtenerPedidoAsync(string csid);

        Task<IEnumerable<PedWebCab>> GetAllPedidos();

        Task<int> ObtenerUltimoNroCbtAsync(short codTipCbt, short cemCbt);
    }
}
