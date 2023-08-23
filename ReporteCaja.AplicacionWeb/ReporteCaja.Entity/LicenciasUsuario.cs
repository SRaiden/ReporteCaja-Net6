using System;
using System.Collections.Generic;

namespace ReporteCaja.Entity;

public partial class LicenciasUsuario
{
    public int Codigo { get; set; }

    public string? Usuario { get; set; }

    public string? Contraseña { get; set; }
}
