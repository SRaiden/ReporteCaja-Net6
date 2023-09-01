using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.FormasPago
{
    public class SFCajaDetalleFormaPago
    {
        public string Descripcion { get; set; }

        public decimal Total { get; set; }

        public int Cantidad { get; set; }
    }
}
