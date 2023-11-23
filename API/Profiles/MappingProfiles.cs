using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Persistence.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() // Remember adding : Profile in the class
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
    
            CreateMap<DetallePedido, DetallePedidoDto>().ReverseMap();
    
            CreateMap<Empleado, EmpleadoDto>().ReverseMap();
    
            CreateMap<GamaProducto, GamaProductoDto>().ReverseMap();
    
            CreateMap<Oficina, OficinaDto>().ReverseMap();
    
            CreateMap<Pago, PagoDto>().ReverseMap();
    
            CreateMap<Pedido, PedidoDto>().ReverseMap();
    
            CreateMap<Producto, ProductoDto>().ReverseMap();
    
        }
    }
}
