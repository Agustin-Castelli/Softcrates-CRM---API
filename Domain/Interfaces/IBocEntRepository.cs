using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBocEntRepository
    {
        Task<IEnumerable<BocEnt>> GetAllAsync();
        Task<IEnumerable<BocEnt>> GetByClienteAsync(int codCli);
        Task<BocEnt?> GetByIdAsync(int codCli, int nroBocEnt);
        Task<BocEnt> AddAsync(BocEnt bocaEntrega);
        Task UpdateAsync(BocEnt bocaEntrega);
        Task DeleteAsync(int codCli, int nroBocEnt);
    }
}
