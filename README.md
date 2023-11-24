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
