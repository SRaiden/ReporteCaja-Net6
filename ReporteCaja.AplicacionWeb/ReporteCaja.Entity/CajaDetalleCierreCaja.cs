using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class CajaDetalleCierreCaja
{
    public int Id { get; set; }

    public int? NroCierre { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Total { get; set; }

    public int? Cantidad { get; set; }

    public int? EmpresaId { get; set; }

    public int? SucursalId { get; set; }

}
