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
    public class OficinaController : BaseController
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        
            public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
            {
                var oficinas = await _unitOfWork.Oficinas.GetAllAsync();
        
                //var paises = await _unitOfWork.Paises.GetAllAsync();
                return _mapper.Map<List<OficinaDto>>(oficinas);
            }
        
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<OficinaDto>> Post(OficinaDto OficinaDto)
            {
                var Oficina = _mapper.Map<Oficina>(OficinaDto);
                this._unitOfWork.Oficinas.Add(Oficina);
                await _unitOfWork.SaveAsync();
                if (Oficina == null)
                {
                    return BadRequest();
                }
                OficinaDto.Id = Oficina.Id;
                return CreatedAtAction(nameof(Post), new { id = OficinaDto.Id }, OficinaDto);
            }
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<OficinaDto>> Get(int id)
            {
                var Oficina = await _unitOfWork.Oficinas.GetByIdAsync(id);
                if (Oficina == null){
                    return NotFound();
                }
                return _mapper.Map<OficinaDto>(Oficina);
            }
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<OficinaDto>> Put(int id, [FromBody] OficinaDto OficinaDto)
            {
                if (OficinaDto == null)
                {
                    return NotFound();
                }
                var oficinas = _mapper.Map<Oficina>(OficinaDto);
                _unitOfWork.Oficinas.Update(oficinas);
                await _unitOfWork.SaveAsync();
                return OficinaDto;
            }
        
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var Oficina = await _unitOfWork.Oficinas.GetByIdAsync(id);
                if (Oficina == null)
                {
                    return NotFound();
                }
                _unitOfWork.Oficinas.Remove(Oficina);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
        }
}