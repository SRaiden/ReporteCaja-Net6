using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class CajaCierresz
{
    public int Id { get; set; }

    public int? Numero { get; set; }

    public DateTime? Fecha { get; set; }

    public int? TicketsCantidadEmitidos { get; set; }

    public int? TicketsCantidadCancelados { get; set; }

    public decimal? TicketsTotal { get; set; }

    public decimal? TicketsTotalExcento { get; set; }

    public decimal? TicketsTotalGravado { get; set; }

    public decimal? TicketsTotalIva { get; set; }

    public decimal? TicketsTotalNoGravado { get; set; }

    public decimal? TicketsTotalTributos { get; set; }

    public int? NcCantidadEmitidas { get; set; }

    public int? NcCantidadCanceladas { get; set; }

    public decimal? NcTotal { get; set; }

    public decimal? NcTotalExcento { get; set; }

    public decimal? NcTotalGravado { get; set; }

    public decimal? NcTotalIva { get; set; }

    public decimal? NcTotalNoGravado { get; set; }

    public decimal? NcTotalTributos { get; set; }

    public int? EmpresaId { get; set; }

    public int? SucursalId { get; set; }
}
