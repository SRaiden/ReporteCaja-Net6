using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class CajaTicketsDiario
{
    public int Id { get; set; }

    public int? NroCierre { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public int? CantidadMostrador { get; set; }

    public decimal? TotalMostrador { get; set; }

    public int? CantidadDelivery { get; set; }

    public decimal? TotalDelivery { get; set; }

    public int? EmpresaId { get; set; }

    public int? SucursalId { get; set; }
}
