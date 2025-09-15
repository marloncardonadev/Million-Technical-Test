using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Api.Filters;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Domain.Entities;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ValidationActionFilter))]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IMapper _mapper;

    public PropertyController(IPropertyService propertyService, IMapper mapper)
    {
        _propertyService = propertyService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertySummaryDto>>> Get([FromQuery] PropertyFilterDto filterDto)
    {
        var result = await _propertyService.GetFilteredAsync(filterDto);
        return Ok(result);
    }

    [HttpGet("GetFiltered")]
    public async Task<IActionResult> GetFiltered([FromQuery] PropertyFilterDto filterDto)
    {

        var properties = await _propertyService.GetFilteredAsync(filterDto);

        if (properties == null || !properties.Items.Any())
        {
            return NotFound("No se encontraron propiedades que coincidan con los filtros.");
        }

        return Ok(properties);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {

        var property = await _propertyService.GetByIdAsync(id);

        if (property == null)
        {
            return NotFound($"No se encontró la propiedad con ID {id}");
        }

        return Ok(property);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(PropertyDto dto)
    {
        var entity = _mapper.Map<Property>(dto);

        await _propertyService.CreateAsync(entity);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, PropertyDto dto)
    {
        var entity = _mapper.Map<Property>(dto);
        entity.Id = id;

        await _propertyService.UpdateAsync(id, entity);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        await _propertyService.DeleteAsync(id);
        return Ok();
    }
}
