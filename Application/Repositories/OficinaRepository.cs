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
    public class OficinaRepository : GenericRepository<Oficina>,IOficina
    {
        private readonly FiltroContext _context;
    
        public OficinaRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }
    
    }
}