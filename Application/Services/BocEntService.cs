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
    public class BocEntService : IBocEntService
    {
        private readonly IBocEntRepository _repository;

        public BocEntService(IBocEntRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BocEntDTO>> GetAllAsync()
        {
            var bocas = await _repository.GetAllAsync();
            return bocas.Select(MapToDTO);
        }

        public async Task<IEnumerable<BocEntDTO>> GetByClienteAsync(int codCli)
        {
            var bocas = await _repository.GetByClienteAsync(codCli);
            return bocas.Select(MapToDTO);
        }

        public async Task<BocEntDTO?> GetByIdAsync(int codCli, int nroBocEnt)
        {
            var boca = await _repository.GetByIdAsync(codCli, nroBocEnt);
            return boca != null ? MapToDTO(boca) : null;
        }

        public async Task<bool> ExistsAsync(int codCli, int nroBocEnt)
        {
            var boca = await _repository.GetByIdAsync(codCli, nroBocEnt);
            return boca != null;
        }

        private static BocEntDTO MapToDTO(BocEnt entity)
        {
            return new BocEntDTO
            {
                CodCli = entity.CodCli,
                NroBocEnt = entity.NroBocEnt,
                NomBocEnt = entity.NomBocEnt,
                DomBocEnt = entity.DomBocEnt
            };
        }
    }
}
