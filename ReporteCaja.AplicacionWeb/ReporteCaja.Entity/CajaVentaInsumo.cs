using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class CajaVentaInsumo
{
    public int Cierre { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Producto { get; set; }

    public string? Nombre { get; set; }

    public string? Categoria { get; set; }

    public string? SubCategoria { get; set; }

    public decimal? FactorUnidad { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public int? EmpresaId { get; set; }

    public int? SucursalId { get; set; }
}
