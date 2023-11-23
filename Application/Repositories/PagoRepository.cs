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
    public class PagoRepository : GenericRepository<Pago>, IPago
    {
        private readonly FiltroContext _context;

        public PagoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pagos2008Paypal>> GetPagos2008Paypal()
        {
            return await (from pago in _context.Pagos
                          where pago.FechaPago.Year == 2008 && pago.FormaPago == "Paypal"
                          orderby pago.Total descending
                          select new Pagos2008Paypal
                          {
                              IdTransaccion = pago.IdTransaccion,
                              FormaPago = pago.FormaPago,
                              FechaPago = pago.FechaPago,
                              Total=pago.Total
                          }).ToListAsync();
        }
    }
}