using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyImageController : ControllerBase
{
    private readonly IPropertyImageRepository _propertyImageRepository;
    private readonly IMapper _mapper;

    public PropertyImageController(IPropertyImageRepository propertyImageRepository, IMapper mapper)
    {
        _propertyImageRepository = propertyImageRepository;
        _mapper = mapper;
    }

    [HttpGet("property/{propertyId}")]
    public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetByProperty(string propertyId)
    {
        var images = await _propertyImageRepository.GetByPropertyIdAsync(propertyId);
        return Ok(_mapper.Map<IEnumerable<PropertyImageDto>>(images));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyImageDto>> GetById(string id)
    {
        var image = await _propertyImageRepository.GetByIdAsync(id);
        if (image == null) return NotFound();
        return Ok(_mapper.Map<PropertyImageDto>(image));
    }

    [HttpPost]
    public async Task<ActionResult<PropertyImageDto>> Create(PropertyImageDto dto)
    {
        var entity = _mapper.Map<PropertyImage>(dto);
        await _propertyImageRepository.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<PropertyImageDto>(entity));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _propertyImageRepository.DeleteAsync(id);
        return NoContent();
    }
}
