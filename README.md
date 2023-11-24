# EndPoints,Consultas realizadas

Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal.Ordene el resultado de mayor a menor.

```

Pagos2008Paypal/Queries/Entities/Domain
public class Pagos2008Paypal
    {
        public string IdTransaccion { get; set; } = null!;
        public string FormaPago { get; set; } = null!;
        public DateOnly FechaPago { get; set; }
        public decimal Total { get; set; }
    }

IPago/Interfaces/Domain
Task<IEnumerable<Pagos2008Paypal>> GetPagos2008Paypal();

PagoRepository/Repositories/Application
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

PagoController/Controllers/API
[HttpGet("Pagos2008Paypal")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<Pagos2008Paypal>>> GetPagos2008Paypal()
{
    var entity = await _unitOfWork.Pagos.GetPagos2008Paypal();
    return _mapper.Map<List<Pagos2008Paypal>>(entity);
}

```

Devuelve un listado con todas las formas de pago que aparecen en la tabla pago.Tenga en cuenta que no deben aparecer formas de pago repetidas.

```

FormasPago/Queries/Entities/Domain
public class FormasPago
{
    public string FormaPago { get; set; } = null!;
}

IPago/Interfaces/Domain
Task<IEnumerable<FormasPago>> GetFormasPago();

PagoRepository/Repositories/Application
public async Task<IEnumerable<FormasPago>> GetFormasPago()
{
    var resultados = await _context.Pagos
        .GroupBy(pago => pago.FormaPago)
        .Select(groupPagos => new FormasPago
        {
            FormaPago = groupPagos.Key
        })
        .ToListAsync();

    return resultados.DistinctBy(dto => dto.FormaPago);
}

PagoController/Controllers/API
[HttpGet("FormasPago")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<FormasPago>>> GetFormasPago()
{
    var entity = await _unitOfWork.Pagos.GetFormasPago();
    return _mapper.Map<List<FormasPago>>(entity);
}

```

Devuelve el nombre de los clientes que hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante

```
ClientesPagosRepresentantes/Queries/Entities/Domain
public class ClientesPagosRepresentantes
    {
        public string NombreCliente { get; set; }
        public string NombreRepresentante { get; set; }
        public string CiudadOficina { get; set; }
    }

ICliente/Interfaces/Domain
Task<IEnumerable<ClientesPagosRepresentantes>> GetClientesPagosRepresentantes();

ClienteRepository/Repositories/Application
public  async Task<IEnumerable<ClientesPagosRepresentantes>> GetClientesPagosRepresentantes()
{
    return await(from cliente in _context.Clientes
                 join Pago in _context.Pagos
                 on cliente.CodigoCliente equals Pago.CodigoCliente
                 join representante in _context.Empleados
                 on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                 join oficina in _context.Oficinas
                 on representante.CodigoOficina equals oficina.CodigoOficina
                 select new ClientesPagosRepresentantes
                 {
                    NombreCliente = cliente.NombreCliente,
                    NombreRepresentante = representante.Nombre,
                    CiudadOficina = oficina.Ciudad
                 }
    ).ToListAsync();
}

ClienteController/Controllers/API
[HttpGet("ClientesPagosRepresentantes")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<ClientesPagosRepresentantes>>> GetClientesPagosRepresentantes()
{
    var clientes = await _unitOfWork.Clientes.GetClientesPagosRepresentantes();
    return _mapper.Map<List<ClientesPagosRepresentantes>>(clientes);
}

```

Devuelve un listado que muestre el nombre de cada empleado,el nombre de su jefe y el nombre del jefe de sus jefe

```

JerarquiaEmpleados/Queries/Entities/Domain
public class JerarquiaEmpleados
{
    public string NombreEmpleado { get; set; }
    public string NombreJefe { get; set; }
    public string NombreJefeDeJefe { get; set; }
}

IEmpleado/Interfaces/Domain
Task<IEnumerable<JerarquiaEmpleados>> GetJerarquiaEmpleados();

EmpleadoRepository/Repositories/Application
public async Task<IEnumerable<JerarquiaEmpleados>> GetJerarquiaEmpleados()
{
    return await (
        from empleado in _context.Empleados
        join jefe1 in _context.Empleados on empleado.CodigoJefe equals jefe1.CodigoEmpleado into jefe1Group
        from jefePrimero in jefe1Group.DefaultIfEmpty()
        join jefe2 in _context.Empleados on jefePrimero.CodigoJefe equals jefe2.CodigoEmpleado into jefe2Group
        from jefeSegundo in jefe2Group.DefaultIfEmpty()
        select new JerarquiaEmpleados
        {
            NombreEmpleado = empleado.Nombre,
            NombreJefe = jefePrimero != null ? jefePrimero.Nombre : null,
            NombreJefeDeJefe = jefeSegundo != null ? jefeSegundo.Nombre : null
        }
    ).ToListAsync();
}

EmpleadoController/Controllers/API
[HttpGet("JerarquiaEmpleados")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<JerarquiaEmpleados>>> GetJerarquiaEmpleados()
{
    var entity = await _unitOfWork.Empleados.GetJerarquiaEmpleados();
    return _mapper.Map<List<JerarquiaEmpleados>>(entity);
}

```
