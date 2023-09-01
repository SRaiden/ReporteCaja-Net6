using System.ComponentModel.DataAnnotations;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Productos
{
    public class SFProductos
    {
        public int Codigo { get; set; }

        public string Nombre { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal { get; set; }
    }
}
