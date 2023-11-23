using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence.Entities;

namespace Domain.Interfaces
{
    public interface IPago:IGenericRepository<Pago>
    {
        Task<IEnumerable<Pagos2008Paypal>> GetPagos2008Paypal();
        
        
    }
}





















