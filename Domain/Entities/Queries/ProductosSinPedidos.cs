using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Queries
{
    public class ProductosSinPedidos
    {
        public string CodigoProducto { get; set; } = null!;

        public string Nombre { get; set; } = null!;
        public string Gama { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}