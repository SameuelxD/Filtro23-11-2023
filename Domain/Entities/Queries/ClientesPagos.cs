using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Queries
{
    public class ClientesPagos
    {
        public int CodigoCliente { get; set; }
        public int ClientePagoFk { get; set; }
        public string NombreCliente { get; set; }
        public string NombreRepresentante { get; set; }
        public string CiudadOficina { get; set; }
    }
}