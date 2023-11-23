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
    public class PedidoController : BaseController
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        
            public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
            {
                var pedidos = await _unitOfWork.Pedidos.GetAllAsync();
        
                //var paises = await _unitOfWork.Paises.GetAllAsync();
                return _mapper.Map<List<PedidoDto>>(pedidos);
            }
        
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<PedidoDto>> Post(PedidoDto PedidoDto)
            {
                var Pedido = _mapper.Map<Pedido>(PedidoDto);
                this._unitOfWork.Pedidos.Add(Pedido);
                await _unitOfWork.SaveAsync();
                if (Pedido == null)
                {
                    return BadRequest();
                }
                PedidoDto.Id = Pedido.Id;
                return CreatedAtAction(nameof(Post), new { id = PedidoDto.Id }, PedidoDto);
            }
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<PedidoDto>> Get(int id)
            {
                var Pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
                if (Pedido == null){
                    return NotFound();
                }
                return _mapper.Map<PedidoDto>(Pedido);
            }
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto PedidoDto)
            {
                if (PedidoDto == null)
                {
                    return NotFound();
                }
                var pedidos= _mapper.Map<Pedido>(PedidoDto);
                _unitOfWork.Pedidos.Update(pedidos);
                await _unitOfWork.SaveAsync();
                return PedidoDto;
            }
        
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var Pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
                if (Pedido == null)
                {
                    return NotFound();
                }
                _unitOfWork.Pedidos.Remove(Pedido);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
        }
}