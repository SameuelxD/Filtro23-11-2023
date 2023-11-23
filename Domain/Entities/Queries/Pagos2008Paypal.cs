using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Queries
{
    public class Pagos2008Paypal
    {
        public string IdTransaccion { get; set; } = null!;
        public string FormaPago { get; set; } = null!;
        public DateOnly FechaPago { get; set; }
        public decimal Total { get; set; }
    }
}