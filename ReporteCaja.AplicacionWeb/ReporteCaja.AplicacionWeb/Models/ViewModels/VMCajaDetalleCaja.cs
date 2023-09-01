using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm;

namespace ReporteCaja.AplicacionWeb.Models.ViewModels
{
    public class VMCajaDetalleCaja
    {
        public VMCajaCierreCaja VMCajaCierreCaja { get; set; } // cabecera

        public int Id { get; set; }

        public int? NroCierre { get; set; }

        public string? Fecha { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Total { get; set; }

        public int? Cantidad { get; set; }

        public int? EmpresaId { get; set; }

        public int? SucursalId { get; set; }
    }
}
