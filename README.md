# EndPoints,Consultas realizadas
Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal.Ordene el resultado de mayor a menor.

```
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

