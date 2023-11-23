using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Queries
{
    public class JefesEmpleados
    {
        public int CodigoEmpleado { get; set; }
        public int? CodigoJefe { get; set; }
        //public int? CodigoJefeDelJefe { get; set; }
        public string? Puesto { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido1 { get; set; } = null!;

        public string? Apellido2 { get; set; }
    }
}