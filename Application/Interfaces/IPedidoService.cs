using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedWebCab> CrearPedidoAsync(CrearPedidoDto dto);
        Task<List<object>> SyncPedidosCreadosAsync(List<CrearPedidoDto> pedidosDto);
        Task<IEnumerable<PedWebCab>> ObtenerHistorialAsync(int codCli, int pageNumber, int pageSize);
        Task<IEnumerable<PedWebCab>> GetAllPedidos();
        Task<PedWebCab?> ObtenerPedidoAsync(string csid);
    }
}
