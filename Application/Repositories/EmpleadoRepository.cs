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
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private readonly FiltroContext _context;

        public EmpleadoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JefesEmpleados>> GetJefesEmpleados()
        {
            return await (from empleado in _context.Empleados
                          select new JefesEmpleados
                         {
                            CodigoEmpleado=empleado.CodigoEmpleado,
                            CodigoJefe=empleado.CodigoJefe,
                            Puesto=empleado.Puesto,
                            Nombre=empleado.Nombre,
                            Apellido1=empleado.Apellido1,
                            Apellido2=empleado.Apellido2,
                            
                         }
                         ).ToListAsync();
        }
    }
    }