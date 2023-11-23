using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Queries;
using Persistence.Entities;

namespace Domain.Interfaces
{
    public interface IEmpleado:IGenericRepository<Empleado>
    {
        Task<IEnumerable<JefesEmpleados>> GetJefesEmpleados();
    }
}