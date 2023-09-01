using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.TicketsDiarios
{
    public class SFDetallesTicketsDiarios
    {
        public int NroCierre { get; set; }

        public string Fecha { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }

        public int CantidadMostrador { get; set; }

        public decimal TotalMostrador { get; set; }

        public int CantidadDelivery { get; set; }

        public decimal TotalDelivery { get; set; }
    }
}
