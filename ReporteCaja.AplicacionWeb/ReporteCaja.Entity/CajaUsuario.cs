using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class CajaUsuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Contraseña { get; set; }

    public string? Rol { get; set; }

    public bool? Activo { get; set; }

    public int? EmpresaId { get; set; }

    public int? SucursalId { get; set; }
}
