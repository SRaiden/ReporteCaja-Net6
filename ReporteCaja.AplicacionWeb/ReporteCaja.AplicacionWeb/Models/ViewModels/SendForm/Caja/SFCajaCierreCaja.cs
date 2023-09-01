using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Caja
{
    public class SFCajaCierreCaja
    {

        public SFCajaCabeceraCaja sfCajaCabeceraCaja { get; set; } // cabeceras

        public List<SFCajaDetalleCaja> sfCajaDetalleCaja { get; set; } // detalle

        public string? nombreSucursal { get; set; }

        public decimal? TotalFinalCaja { get; set; }

    }
}
