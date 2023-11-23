using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace API.Controllers
{
    public class DetallePedidoController : BaseController
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        
            public DetallePedidoController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
            {
                var detallePedidos = await _unitOfWork.DetallePedidos.GetAllAsync();
        
                //var paises = await _unitOfWork.Paises.GetAllAsync();
                return _mapper.Map<List<DetallePedidoDto>>(detallePedidos);
            }
        
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<DetallePedidoDto>> Post(DetallePedidoDto DetallePedidoDto)
            {
                var DetallePedido = _mapper.Map<DetallePedido>(DetallePedidoDto);
                this._unitOfWork.DetallePedidos.Add(DetallePedido);
                await _unitOfWork.SaveAsync();
                if (DetallePedido == null)
                {
                    return BadRequest();
                }
                DetallePedidoDto.Id = DetallePedido.Id;
                return CreatedAtAction(nameof(Post), new { id = DetallePedidoDto.Id }, DetallePedidoDto);
            }
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<DetallePedidoDto>> Get(int id)
            {
                var DetallePedido = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
                if (DetallePedido == null){
                    return NotFound();
                }
                return _mapper.Map<DetallePedidoDto>(DetallePedido);
            }
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody] DetallePedidoDto DetallePedidoDto)
            {
                if (DetallePedidoDto == null)
                {
                    return NotFound();
                }
                var detallePedidos = _mapper.Map<DetallePedido>(DetallePedidoDto);
                _unitOfWork.DetallePedidos.Update(detallePedidos);
                await _unitOfWork.SaveAsync();
                return DetallePedidoDto;
            }
        
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var DetallePedido = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
                if (DetallePedido == null)
                {
                    return NotFound();
                }
                _unitOfWork.DetallePedidos.Remove(DetallePedido);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
        }
}