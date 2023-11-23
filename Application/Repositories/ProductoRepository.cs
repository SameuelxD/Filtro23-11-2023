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
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly FiltroContext _context;

        public ProductoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductosSinPedidos>> GetProductosSinPedidos()

        {
            return await (from producto in _context.Productos
                          join DetallePedido in _context.DetallePedidos
                          on producto.CodigoProducto equals DetallePedido.CodigoProducto into GrupoProductos
                          from prod in GrupoProductos.DefaultIfEmpty()
                          where prod == null
                          select new ProductosSinPedidos
                          {
                              CodigoProducto = prod.CodigoProducto,
                              Nombre = producto.Nombre,
                              Gama = producto.Gama,
                              Descripcion = producto.Descripcion
                          }
            ).ToListAsync();


        }

    }
}



