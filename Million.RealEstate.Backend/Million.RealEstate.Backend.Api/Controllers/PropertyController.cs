using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Api.Filters;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Core.DTOs;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ValidationActionFilter))]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
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
}
