namespace ReporteCaja.AplicacionWeb.Models.ViewModels
{
    public class VMPedidos
    {
        public int idUsuario { get; set; }

        public int idSucursal { get; set; }

        public int idEmpresa { get; set; }

        public string? tipo { get; set; }

        public string? FechaDesde { get; set; }

        public string? FechaHasta { get; set; }

    }
}
