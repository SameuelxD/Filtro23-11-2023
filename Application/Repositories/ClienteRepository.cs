using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Queries;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        private readonly FiltroContext _context;

        public ClienteRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<ClientesPagos>> GetClientesPagos()
        {
            return await(from cliente in _context.Clientes
                         join Pago in _context.Pagos
                         on cliente.CodigoCliente equals Pago.CodigoCliente
                         join empleado in _context.Empleados
                         on cliente.CodigoEmpleadoRepVentas equals empleado.CodigoEmpleado
                         join of in _context.Oficinas
                         on empleado.CodigoOficina equals of.CodigoOficina
                         select new ClientesPagos
                         {
                             CodigoCliente = cliente.CodigoCliente,
                             ClientePagoFk = Pago.CodigoCliente,
                             NombreCliente = cliente.NombreCliente,
                             NombreRepresentante = empleado.Nombre,
                             CiudadOficina = of.Ciudad
                         }
            ).ToListAsync();
        }
    }
}