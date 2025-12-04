using Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBonificacionService
    {
        /// <summary>
        /// Obtiene el porcentaje de bonificación para un cliente y artículo específico
        /// basándose en cantidad o importe
        /// </summary>
        Task<decimal> ObtenerPorcentajeBonificacionAsync(int codCli, string codArt, decimal cantidad, decimal precioUnitario);

        /// <summary>
        /// Obtiene la bonificación base (primer escalón) para un cliente y artículo
        /// </summary>
        Task<BonificacionDTO?> ObtenerBonificacionBaseAsync(int codCli, string codArt);

        /// <summary>
        /// Obtiene todos los artículos con sus bonificaciones para un cliente
        /// </summary>
        Task<IEnumerable<ArticuloConBonificacionDTO>> ObtenerArticulosConBonificacionAsync(int codCli);

        /// <summary>
        /// Calcula el subtotal con bonificación aplicada
        /// </summary>
        Task<decimal> CalcularSubtotalConBonificacionAsync(int codCli, string codArt, decimal cantidad, decimal precioUnitario);
    }
}
