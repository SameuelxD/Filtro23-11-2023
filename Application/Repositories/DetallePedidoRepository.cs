using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repositories
{
    public class DetallePedidoRepository : GenericRepository<DetallePedido>,IDetallePedido
    {
        private readonly FiltroContext _context;
    
        public DetallePedidoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }
    
       
    }
}