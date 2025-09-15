using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Api.Filters;
using Million.RealEstate.Backend.Application.Interfaces;

namespace Million.RealEstate.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ValidationActionFilter))]
public class PropertyTraceController : ControllerBase
{
    private readonly IPropertyTraceRepository _propertyTraceRepository;

    public PropertyTraceController(IPropertyTraceRepository propertyTraceRepository)
    {
        _propertyTraceRepository = propertyTraceRepository;
    }

    [HttpGet("byProperty/{propertyId}")]
    public async Task<IActionResult> GetByProperty(string propertyId)
    {
        var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(propertyId);

        if (propertyTraces == null || !propertyTraces.Any())
        {
            return NotFound("No se encontraron trace de propiedades que coincidan con los filtros.");
        }

        return Ok(propertyTraces);
    }
}
