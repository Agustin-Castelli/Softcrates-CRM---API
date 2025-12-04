using Application.Interfaces;
using Application.Models.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class BonificacionService : IBonificacionService
    {
        private readonly IBonArtCliRepository _bonArtCliRepo;
        private readonly IBonClaDetRepository _bonClaDetRepo;
        private readonly IArticuloRepository _articuloRepo;
        private readonly IPreVRepository _preVenRepo;
        private readonly ILogger<BonificacionService> _logger;

        public BonificacionService(
            IBonArtCliRepository bonArtCliRepo,
            IBonClaDetRepository bonClaDetRepo,
            IArticuloRepository articuloRepo,
            IPreVRepository preVenRepo,
            ILogger<BonificacionService> logger)
        {
            _bonArtCliRepo = bonArtCliRepo;
            _bonClaDetRepo = bonClaDetRepo;
            _articuloRepo = articuloRepo;
            _preVenRepo = preVenRepo;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el porcentaje de bonificación por IMPORTE para un cliente y artículo
        /// </summary>
        public async Task<decimal> ObtenerPorcentajeBonificacionAsync(int codCli, string codArt, decimal cantidad, decimal precioUnitario)
        {
            try
            {
                // 1. Buscar si el cliente tiene bonificación para este artículo
                var bonArtCli = await _bonArtCliRepo.GetByClienteYArticuloAsync(codCli, codArt);

                if (bonArtCli == null || bonArtCli.Inactivo)
                {
                    _logger?.LogInformation($"[BONIFICACION] No hay bonificación activa para cliente {codCli} y artículo {codArt}");
                    return 0;
                }

                // 2. Obtener los escalones de bonificación
                var escalones = await _bonClaDetRepo.GetByCodClaBonAsync(bonArtCli.CodClaBon);

                if (!escalones.Any())
                {
                    _logger?.LogWarning($"[BONIFICACION] No hay escalones para CodClaBon {bonArtCli.CodClaBon}");
                    return 0;
                }

                // 3. Calcular importe total
                decimal importeTotal = cantidad * precioUnitario;

                // 4. Buscar el escalón que corresponda según el importe
                var escalonAplicable = escalones
                    .Where(e => e.ValEscDes <= importeTotal && (e.ValEscHas == null || importeTotal <= e.ValEscHas))
                    .OrderByDescending(e => e.ValEscDes) // Tomar el escalón más alto que aplique
                    .FirstOrDefault();

                if (escalonAplicable == null)
                {
                    // Si no hay escalón aplicable, tomar el primer escalón (sin escala)
                    escalonAplicable = escalones.OrderBy(e => e.Secuencia).FirstOrDefault();
                }

                var porcentaje = escalonAplicable?.PorBonImp ?? 0;

                _logger?.LogInformation($"[BONIFICACION] Cliente {codCli}, Art {codArt}: {porcentaje}% (Importe: {importeTotal})");

                return porcentaje;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"[BONIFICACION] Error calculando bonificación para cliente {codCli} y artículo {codArt}");
                return 0;
            }
        }

        /// <summary>
        /// Obtiene la bonificación base (primer escalón) para mostrar en el listado
        /// </summary>
        public async Task<BonificacionDTO?> ObtenerBonificacionBaseAsync(int codCli, string codArt)
        {
            try
            {
                var bonArtCli = await _bonArtCliRepo.GetByClienteYArticuloAsync(codCli, codArt);

                if (bonArtCli == null || bonArtCli.Inactivo)
                    return null;

                var escalones = await _bonClaDetRepo.GetByCodClaBonAsync(bonArtCli.CodClaBon);
                var primerEscalon = escalones.OrderBy(e => e.Secuencia).FirstOrDefault();

                if (primerEscalon == null)
                    return null;

                var porcentaje = primerEscalon.PorBonImp ?? 0;

                return new BonificacionDTO
                {
                    CodClaBon = bonArtCli.CodClaBon,
                    PorcentajeBonificacion = porcentaje,
                    TipoEscala = primerEscalon.TipEsc.HasValue ? primerEscalon.TipEsc.Value.ToString() : "N" // 🔥 CORREGIDO
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"[BONIFICACION] Error obteniendo bonificación base para cliente {codCli} y artículo {codArt}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los artículos con bonificaciones para un cliente específico
        /// </summary>
        public async Task<IEnumerable<ArticuloConBonificacionDTO>> ObtenerArticulosConBonificacionAsync(int codCli)
        {
            try
            {
                // 1. Obtener todos los artículos
                var articulos = await _articuloRepo.GetAll();

                // 2. Obtener bonificaciones del cliente
                var bonificacionesCliente = await _bonArtCliRepo.GetByClienteAsync(codCli);
                var dictBonificaciones = bonificacionesCliente.ToDictionary(b => b.CodArt, b => b.CodClaBon);

                // 3. Construir lista con bonificaciones
                var resultado = new List<ArticuloConBonificacionDTO>();

                foreach (var art in articulos)
                {
                    // 🔥 CORREGIDO: Obtener precio desde PreVen mediante CodArt
                    var preVen = await _preVenRepo.ObtenerPrecioAsync(art.CodArt); // Asumiendo que el método se llama así
                    var precio = preVen ?? 0;

                    var artConBon = new ArticuloConBonificacionDTO
                    {
                        CodArt = art.CodArt,
                        DesArt = art.DesArt,
                        Precio = precio // 🔥 CORREGIDO
                    };

                    // Si tiene bonificación, obtener el porcentaje base
                    if (dictBonificaciones.TryGetValue(art.CodArt, out var codClaBon))
                    {
                        var bonificacion = await ObtenerBonificacionBaseAsync(codCli, art.CodArt);
                        if (bonificacion != null)
                        {
                            artConBon.CodClaBon = bonificacion.CodClaBon;
                            artConBon.PorcentajeBonificacion = bonificacion.PorcentajeBonificacion;
                        }
                    }

                    resultado.Add(artConBon);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"[BONIFICACION] Error obteniendo artículos con bonificación para cliente {codCli}");
                return Enumerable.Empty<ArticuloConBonificacionDTO>();
            }
        }

        /// <summary>
        /// Calcula el subtotal con bonificación aplicada
        /// </summary>
        public async Task<decimal> CalcularSubtotalConBonificacionAsync(int codCli, string codArt, decimal cantidad, decimal precioUnitario)
        {
            var porcentaje = await ObtenerPorcentajeBonificacionAsync(codCli, codArt, cantidad, precioUnitario);
            var subtotal = cantidad * precioUnitario;
            var descuento = subtotal * (porcentaje / 100);
            var subtotalFinal = subtotal - descuento;

            _logger?.LogInformation($"[BONIFICACION] Subtotal: {subtotal}, Descuento ({porcentaje}%): {descuento}, Final: {subtotalFinal}");

            return subtotalFinal;
        }
    }
}
