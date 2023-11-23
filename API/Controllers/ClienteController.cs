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
    public class ClienteController : BaseController
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        
            public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
            {
                var clientes = await _unitOfWork.Clientes.GetAllAsync();
        
                //var paises = await _unitOfWork.Paises.GetAllAsync();
                return _mapper.Map<List<ClienteDto>>(clientes);
            }
        
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<ClienteDto>> Post(ClienteDto ClienteDto)
            {
                var cliente = _mapper.Map<Cliente>(ClienteDto);
                this._unitOfWork.Clientes.Add(cliente);
                await _unitOfWork.SaveAsync();
                if (cliente == null)
                {
                    return BadRequest();
                }
                ClienteDto.Id = cliente.Id;
                return CreatedAtAction(nameof(Post), new { id = ClienteDto.Id }, ClienteDto);
            }
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ClienteDto>> Get(int id)
            {
                var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
                if (cliente == null){
                    return NotFound();
                }
                return _mapper.Map<ClienteDto>(cliente);
            }
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto ClienteDto)
            {
                if (ClienteDto == null)
                {
                    return NotFound();
                }
                var cities = _mapper.Map<Cliente>(ClienteDto);
                _unitOfWork.Clientes.Update(cities);
                await _unitOfWork.SaveAsync();
                return ClienteDto;
            }
        
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var Cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
                if (Cliente == null)
                {
                    return NotFound();
                }
                _unitOfWork.Clientes.Remove(Cliente);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            [HttpGet("ClientesPagos")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<ClientesPagos>>> GetClientesPagos()
            {
                var clientes = await _unitOfWork.Clientes.GetClientesPagos();
                return _mapper.Map<List<ClientesPagos>>(clientes);
            }
        }
}