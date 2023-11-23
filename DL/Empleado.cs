using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? IdArea { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public double? Sueldo { get; set; }

    public virtual Area? IdAreaNavigation { get; set; }
}
