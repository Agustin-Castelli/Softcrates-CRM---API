using Application.Interfaces;
using Application.Models.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepo;

        public PedidoService(IPedidoRepository pedidoRepo)
        {
            _pedidoRepo = pedidoRepo;
        }

        public async Task<PedWebCab> CrearPedidoAsync(CrearPedidoDto dto)
        {
            // 1. Obtener el último nroCbt
            var ultimoNro = await _pedidoRepo.ObtenerUltimoNroCbtAsync(dto.CodTipCbt, dto.CemCbt);
            var nuevoNro = ultimoNro + 1;

            // 2. Crear cabecera
            var pedido = new PedWebCab
            {
                CodTipCbt = dto.CodTipCbt,
                CemCbt = dto.CemCbt,
                NroCbt = nuevoNro,
                CodCli = dto.CodCli,
                FecCbt = DateTime.Now,
                FecEntCbt = DateTime.Now.AddDays(5),  // DE AQUÍ PARA ABAJO SON TODOS DATOS IRRELEVANTES, SE RELLENAN IGUAL QUE COMO ESTÁN EN LA DB (INDICADO POR ARIEL)
                CodIva = 1,
                CodSuc = "1",
                CodList = 1,
                CodClaBon = 1,
                CodVen = "01",
                CodTra = "01",
                CodConVta = "CC",
                CodMon = 1,
                ImpCot = 1,
                ImpGraCbt = 0,
                PorDesCbt = 0,
                ImpDesCbt = 0,
                PorRecCbt = 0,
                ImpRecCbt = 0,
                ImpNetGraCbt = 0,
                ImpIvaCbt = 0,
                ImpTotCbt = 0,
                AutPed = "N",
                UsrIngreso = "Default",   
                FecIngreso = DateTime.Now,
                FecAut = DateTime.Now,
                NroBocEnt = 0,
                PedWebInc = "",
                FecPedWebInc = DateTime.Now,
                FecAutSec = DateTime.Now,
            };

            // 3. Construir PK CSID (concatenación)
            pedido.Csid = $"{pedido.CodTipCbt}{pedido.CemCbt}{pedido.NroCbt}{pedido.CodCli}";

            // 4. Guardar cabecera
            await _pedidoRepo.AgregarAsync(pedido);

            // 5. Guardar detalles
            short secuencia = 1;
            decimal totalPedido = 0;
            foreach (var det in dto.Detalles)
            {
                var subtotal = det.PrecioUnitario * det.Cantidad;

                var detalle = new PedWebArt
                {
                    Csid = pedido.Csid, // FK hacia cabecera
                    Secuencia = secuencia, // 👈 le damos el valor incremental
                    CodArt = det.CodArt,
                    DesArtAmp = det.DesArt,
                    CanArt = det.Cantidad,
                    PreArt = det.PrecioUnitario,
                    CodIvaArt = 78,
                    CodClaBon = 0,
                    FecEntArt = DateTime.Now,
                    ImpBonArt = 0,
                    ImpGraArt = det.PrecioUnitario * det.Cantidad,
                    ImpDesArt = 0,
                    ImpNetGraArt = 0,
                    ImpIvaArt = 0
                };

                await _pedidoRepo.AgregarDetalleAsync(detalle);

                totalPedido += subtotal;
                secuencia++; // 👈 incrementamos para el siguiente producto
            }

            pedido.ImpTotCbt = totalPedido;
            await _pedidoRepo.ActualizarAsync(pedido);

            return pedido;
        }

        public async Task<List<object>> SyncPedidosCreadosAsync(List<CrearPedidoDto> pedidosDto)
        {
            var resultados = new List<object>();

            foreach (var dto in pedidosDto)
            {
                try
                {
                    var pedido = await CrearPedidoAsync(dto);
                    resultados.Add(new { Exito = true, Pedido = pedido });
                }
                catch (Exception ex)
                {
                    resultados.Add(new { Exito = false, Error = ex.Message, Pedido = dto });
                }
            }

            return resultados;
        }

        public async Task<IEnumerable<PedWebCab>> ObtenerHistorialAsync(int codCli, int pageNumber, int pageSize)
        {
            return await _pedidoRepo.ObtenerHistorialAsync(codCli, pageNumber, pageSize);
        }

        public async Task<IEnumerable<PedWebCab>> GetAllPedidos()
        {
            return await _pedidoRepo.GetAllPedidos();
        }

        public async Task<PedWebCab?> ObtenerPedidoAsync(string csid)
        {
            return await _pedidoRepo.ObtenerPedidoAsync(csid);
        }
    }

}
