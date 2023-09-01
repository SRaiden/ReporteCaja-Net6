using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Caja
{
    public class SFCajaDetalleCaja
    {
        public string? DescripcionDetalle { get; set; }

        public decimal? TotalDetalle { get; set; }

        public int? NroCierreDetalle { get; set; }
    }
}
