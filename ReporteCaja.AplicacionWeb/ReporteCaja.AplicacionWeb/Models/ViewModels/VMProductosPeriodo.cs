namespace ReporteCaja.AplicacionWeb.Models.ViewModels
{
    public class VMProductosPeriodo
    {
        public int Id { get; set; }

        public string? Fecha { get; set; } //--//

        public int? Codigo { get; set; }

        public string? Nombre { get; set; }

        public decimal? KilosProducto { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Total { get; set; }

        public decimal? TotalKilos { get; set; }

        public int? NroCierre { get; set; }

        public string? Usuario { get; set; }

        public int? EmpresaId { get; set; }

        public int? SucursalId { get; set; }
    }
}
