using Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBocEntService
    {
        Task<IEnumerable<BocEntDTO>> GetAllAsync();
        Task<IEnumerable<BocEntDTO>> GetByClienteAsync(int codCli);
        Task<BocEntDTO?> GetByIdAsync(int codCli, int nroBocEnt);
        Task<bool> ExistsAsync(int codCli, int nroBocEnt);
    }
}
