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
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepo;
        private readonly IPreVRepository _preVRepo;

        public ArticuloService(IArticuloRepository articuloRepo, IPreVRepository preVRepo)
        {
            _articuloRepo = articuloRepo;
            _preVRepo = preVRepo;
        }

        public async Task<IEnumerable<ArticuloDto>> GetAll() { 
            var articulos = await _articuloRepo.GetAll(); 
            var resultado = new List<ArticuloDto>(); 
            foreach (var art in articulos) 
            { 
                var precio = await _preVRepo.ObtenerPrecioAsync(art.CodArt);
                resultado.Add(new ArticuloDto { CodArt = art.CodArt, DesArt = art.DesArt, Precio = precio ?? 0 });
            } 

            return resultado; 
        }

        public async Task<IEnumerable<ArticuloDto>> ObtenerArticulosAsync(int pageNumber, int pageSize)
        {
            var articulos = await _articuloRepo.ObtenerArticulosAsync(pageNumber, pageSize);

            var resultado = new List<ArticuloDto>();

            foreach (var art in articulos)
            {
                var precio = await _preVRepo.ObtenerPrecioAsync(art.CodArt);
                resultado.Add(new ArticuloDto
                {
                    CodArt = art.CodArt,
                    DesArt = art.DesArt,
                    Precio = precio ?? 0
                });
            }

            return resultado;
        }


        public async Task<IEnumerable<ArticuloDto>> BuscarPorNombreAsync(string nombre)
        {
            var articulos = await _articuloRepo.BuscarPorNombreAsync(nombre);

            var resultado = new List<ArticuloDto>();

            foreach (var art in articulos)
            {
                var precio = await _preVRepo.ObtenerPrecioAsync(art.CodArt);

                resultado.Add(new ArticuloDto
                {
                    CodArt = art.CodArt,
                    DesArt = art.DesArt,
                    Precio = precio ?? 0
                });
            }

            return resultado;
        }

        public async Task<ArticuloDto?> ObtenerPorCodigoAsync(string codArt)
        {
            var art = await _articuloRepo.ObtenerPorCodigoAsync(codArt);
            if (art == null) return null;

            var precio = await _preVRepo.ObtenerPrecioAsync(art.CodArt);

            return new ArticuloDto
            {
                CodArt = art.CodArt,
                DesArt = art.DesArt,
                Precio = precio ?? 0
            };
        }
    }
}
