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
    public class GamaProductoRepository : GenericRepository<GamaProducto>,IGamaProducto
    {
        private readonly FiltroContext _context;
    
        public GamaProductoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }
    
    }
}