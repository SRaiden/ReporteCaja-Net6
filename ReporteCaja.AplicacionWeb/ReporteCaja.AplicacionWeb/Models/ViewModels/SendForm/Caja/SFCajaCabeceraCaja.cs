using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Caja
{
    public class SFCajaCabeceraCaja
    {
        public int? NroCierreCabecera { get; set; }

        public string? FechaCabecera { get; set; }

        public string? HoraCabecera { get; set; }

        public decimal? TotalXCierreDetalle { get; set; }

    }
}
