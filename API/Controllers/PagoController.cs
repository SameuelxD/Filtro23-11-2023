using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities.Queries;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace API.Controllers
{
    public class PagoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
        {
            var Pagos = await _unitOfWork.Pagos.GetAllAsync();

            //var paises = await _unitOfWork.Paises.GetAllAsync();
            return _mapper.Map<List<PagoDto>>(Pagos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoDto>> Post(PagoDto PagoDto)
        {
            var Pago = _mapper.Map<Pago>(PagoDto);
            this._unitOfWork.Pagos.Add(Pago);
            await _unitOfWork.SaveAsync();
            if (Pago == null)
            {
                return BadRequest();
            }
            PagoDto.Id = Pago.Id;
            return CreatedAtAction(nameof(Post), new { id = PagoDto.Id }, PagoDto);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagoDto>> Get(int id)
        {
            var Pago = await _unitOfWork.Pagos.GetByIdAsync(id);
            if (Pago == null)
            {
                return NotFound();
            }
            return _mapper.Map<PagoDto>(Pago);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto PagoDto)
        {
            if (PagoDto == null)
            {
                return NotFound();
            }
            var cities = _mapper.Map<Pago>(PagoDto);
            _unitOfWork.Pagos.Update(cities);
            await _unitOfWork.SaveAsync();
            return PagoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Pago = await _unitOfWork.Pagos.GetByIdAsync(id);
            if (Pago == null)
            {
                return NotFound();
            }
            _unitOfWork.Pagos.Remove(Pago);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("Pagos2008Paypal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pagos2008Paypal>>> GetPagos2008Paypal()
        {
            var entity = await _unitOfWork.Pagos.GetPagos2008Paypal();
            return _mapper.Map<List<Pagos2008Paypal>>(entity);
        }
    }
}