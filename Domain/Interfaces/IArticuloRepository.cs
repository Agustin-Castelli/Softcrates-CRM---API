using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IArticuloRepository
    {
        Task<IEnumerable<Artic>> GetAll();
        Task<IEnumerable<Artic>> ObtenerArticulosAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Artic>> BuscarPorNombreAsync(string nombre);
        Task<Artic?> ObtenerPorCodigoAsync(string codArt);
    }
}
