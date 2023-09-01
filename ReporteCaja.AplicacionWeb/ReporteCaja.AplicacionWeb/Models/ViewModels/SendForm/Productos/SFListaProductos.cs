namespace ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Productos
{
    public class SFListaProductos
    {
        public List<SFProductos> Productos { get; set; }

        public int CantidadTotal { get; set; }

        public decimal Total { get; set; }

        public string NombreSucursal { get; set; }
    }
}
