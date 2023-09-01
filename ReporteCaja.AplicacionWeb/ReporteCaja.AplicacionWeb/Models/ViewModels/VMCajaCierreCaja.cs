namespace ReporteCaja.AplicacionWeb.Models.ViewModels
{
    public class VMCajaCierreCaja
    {
        public int Id { get; set; }

        public int? NroCierre { get; set; }

        public string? Usuario { get; set; }

        public string? Tipo { get; set; }

        public string? Fecha { get; set; }

        public decimal? TotalEfectivo { get; set; }

        public decimal? TotalTarjeta { get; set; }

        public string? Hora { get; set; }

        public decimal? TotalIngreso { get; set; }

        public decimal? TotalEgreso { get; set; }

        public decimal? TotalVentas { get; set; }

        public decimal? TotalCtaCte { get; set; }

        public decimal? InicioProximoTurno { get; set; }

        public decimal? Diferencia { get; set; }

        public int? EmpresaId { get; set; }

        public int? SucursalId { get; set; }
    }
}
