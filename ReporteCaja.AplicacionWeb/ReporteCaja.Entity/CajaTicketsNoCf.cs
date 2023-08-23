using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class CajaTicketsNoCf
{
    public int Id { get; set; }

    public int? Correlativo { get; set; }

    public DateTime? Fecha { get; set; }

    public DateTime? FechaCaja { get; set; }

    public string? Tipo { get; set; }

    public string? Letra { get; set; }

    public int? PuntoVenta { get; set; }

    public int? Numero { get; set; }

    public string? Cuit { get; set; }

    public string? RazonSocial { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Iva { get; set; }

    public decimal? Total { get; set; }

    public int? NroCierre { get; set; }

    public int? EmpresaId { get; set; }

    public int? SucursalId { get; set; }
}
