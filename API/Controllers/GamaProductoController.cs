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
    public class GamaProductoController : BaseController
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        
            public GamaProductoController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<GamaProductoDto>>> Get()
            {
                var gamaProductos = await _unitOfWork.GamaProductos.GetAllAsync();
        
                //var paises = await _unitOfWork.Paises.GetAllAsync();
                return _mapper.Map<List<GamaProductoDto>>(gamaProductos);
            }
        
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<GamaProductoDto>> Post(GamaProductoDto GamaProductoDto)
            {
                var GamaProducto = _mapper.Map<GamaProducto>(GamaProductoDto);
                this._unitOfWork.GamaProductos.Add(GamaProducto);
                await _unitOfWork.SaveAsync();
                if (GamaProducto == null)
                {
                    return BadRequest();
                }
                GamaProductoDto.Id = GamaProducto.Id;
                return CreatedAtAction(nameof(Post), new { id = GamaProductoDto.Id }, GamaProductoDto);
            }
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<GamaProductoDto>> Get(int id)
            {
                var GamaProducto = await _unitOfWork.GamaProductos.GetByIdAsync(id);
                if (GamaProducto == null){
                    return NotFound();
                }
                return _mapper.Map<GamaProductoDto>(GamaProducto);
            }
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<GamaProductoDto>> Put(int id, [FromBody] GamaProductoDto GamaProductoDto)
            {
                if (GamaProductoDto == null)
                {
                    return NotFound();
                }
                var gamaProductos = _mapper.Map<GamaProducto>(GamaProductoDto);
                _unitOfWork.GamaProductos.Update(gamaProductos);
                await _unitOfWork.SaveAsync();
                return GamaProductoDto;
            }
        
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var GamaProducto = await _unitOfWork.GamaProductos.GetByIdAsync(id);
                if (GamaProducto == null)
                {
                    return NotFound();
                }
                _unitOfWork.GamaProductos.Remove(GamaProducto);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
        }
}