using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.TicketsDiarios;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.FormasPago
{
    public class SFFormasPago
    {
        public List<SFCajaDetalleFormaPago> SFCajaDetalleFormaPago { get; set; }

        public string NombreSucursal { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }
    }
}
