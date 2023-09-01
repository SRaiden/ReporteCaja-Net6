using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.CierreZ
{
    public class SFCierreZ
    {
        public int Numero { get; set; }

        public string Tipo { get; set; }

        public string Fecha { get; set; }

        // TICKETS

        public string T_Emitido { get; set; }

        public string T_Cancelado { get; set; }

        public decimal T_Exento { get; set; }

        public decimal T_Gravado { get; set; }

        public decimal T_IVA { get; set; }

        public decimal T_NoGrabado { get; set; }

        public decimal T_TotalTributos { get; set; }

        public decimal T_Total { get; set; }

        // NOTA DE CREDITO

        public string NC_Emitido { get; set; }

        public string NC_Cancelado { get; set; }

        public decimal NC_Exento { get; set; }

        public decimal NC_Gravado { get; set; }

        public decimal NC_IVA { get; set; }

        public decimal NC_NoGrabado { get; set; }

        public decimal NC_TotalTributos { get; set; }

        public decimal NC_Total { get; set; }
    }
}
