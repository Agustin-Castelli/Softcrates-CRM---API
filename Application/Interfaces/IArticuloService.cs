using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArticuloService
    {
        Task<IEnumerable<ArticuloDto>> GetAll();
        Task<IEnumerable<ArticuloDto>> ObtenerArticulosAsync(int pageNumber, int pageSize);
        Task<IEnumerable<ArticuloDto>> BuscarPorNombreAsync(string nombre);
        Task<ArticuloDto?> ObtenerPorCodigoAsync(string codArt);
    }
}
