namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.TicketsDiarios
{
    public class SFTicketsDiarios
    {
        public List<SFDetallesTicketsDiarios> SFDetallesTicketsDiarios { get; set; }

        public string NombreSucursal { get; set; }

        public int CantidadMostradorTotal { get; set; }

        public decimal TotalMostradorTotal { get; set; }

        public int CantidadDeliveryTotal { get; set; }

        public decimal TotalDeliveryTotal { get; set; }

        public int CantidadTotal { get; set; }

        public decimal TotalTotal { get; set; }
    }
}
