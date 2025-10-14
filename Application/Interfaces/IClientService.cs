using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<Client?> GetByIdAsync(int codCli);
        Task<IEnumerable<Client>> SearchByNameAsync(string name);
        Task<ClienteResumenDto?> GetResumenAsync(int codCli);
    }
}
