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
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFacturaRepository _facturaRepository;

        public ClientService(IClientRepository clientRepository, IFacturaRepository facturaRepository)
        {
            _clientRepository = clientRepository;
            _facturaRepository = facturaRepository;
        }

        public async Task<Client?> GetByIdAsync(int codCli)
        {
            return await _clientRepository.GetByIdAsync(codCli);
        }

        public async Task<IEnumerable<Client>> SearchByNameAsync(string name)
        {
            return await _clientRepository.SearchByNameAsync(name);
        }

        public async Task<ClienteResumenDto?> GetResumenAsync(int codCli)
        {
            var client = await _clientRepository.GetByIdAsync(codCli);
            if (client == null) return null;

            var facturas = await _facturaRepository.GetByClientAsync(codCli);

            var dto = new ClienteResumenDto
            {
                CodCli = client.CodCli,
                Nombre = client.NomCli,
                SaldoActual = client.SaldoDeuCc,
                LimiteCredito = client.LimiteCredito,
                SaldoVencido = client.SaldoVencCc,
                PorcentajeCreditoUsado = client.LimiteCreditoUso,
                Facturas = facturas.Select(f => new FacturaDto
                {
                    NumeroFactura = f.CSIDcbtDeu,
                    Fecha = f.FechaCbt,
                    Importe = f.ImporteOriginal,
                    Estado = f.EstadoCC
                }).ToList()
            };

            return dto;
        }
    }
}
