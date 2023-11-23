using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Persistence.Data;
using Persistence.Entities;

namespace Application.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>,IPedido
    {
        private readonly FiltroContext _context;
    
        public PedidoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }
    
    }
}