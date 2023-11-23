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
    public class ProductoController : BaseController
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        
            public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
        
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
            {
                var productos = await _unitOfWork.Productos.GetAllAsync();
        
                //var paises = await _unitOfWork.Paises.GetAllAsync();
                return _mapper.Map<List<ProductoDto>>(productos);
            }
        
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<ProductoDto>> Post(ProductoDto ProductoDto)
            {
                var Producto = _mapper.Map<Producto>(ProductoDto);
                this._unitOfWork.Productos.Add(Producto);
                await _unitOfWork.SaveAsync();
                if (Producto == null)
                {
                    return BadRequest();
                }
                ProductoDto.Id = Producto.Id;
                return CreatedAtAction(nameof(Post), new { id = ProductoDto.Id }, ProductoDto);
            }
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ProductoDto>> Get(int id)
            {
                var Producto = await _unitOfWork.Productos.GetByIdAsync(id);
                if (Producto == null){
                    return NotFound();
                }
                return _mapper.Map<ProductoDto>(Producto);
            }
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto ProductoDto)
            {
                if (ProductoDto == null)
                {
                    return NotFound();
                }
                var producto = _mapper.Map<Producto>(ProductoDto);
                _unitOfWork.Productos.Update(producto);
                await _unitOfWork.SaveAsync();
                return ProductoDto;
            }
        
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var Producto = await _unitOfWork.Productos.GetByIdAsync(id);
                if (Producto == null)
                {
                    return NotFound();
                }
                _unitOfWork.Productos.Remove(Producto);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            [HttpGet("ProductosSinPedidos")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<IEnumerable<ProductosSinPedidos>>> ProductDontSells()
            {
              var productos = await _unitOfWork.Productos.GetProductosSinPedidos();
              return _mapper.Map<List<ProductosSinPedidos>>(productos);
            }
            
        }
}