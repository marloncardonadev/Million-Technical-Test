using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Application.Interfaces;

namespace Million.RealEstate.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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

        return Ok(propertyTraces);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<PropertyTraceDto>> GetById(string id)
    //{
    //    var trace = await _propertyTraceRepository.GetByIdAsync(id);
    //    if (trace == null) return NotFound();
    //    return Ok(_mapper.Map<PropertyTraceDto>(trace));
    //}

    //[HttpPost]
    //public async Task<ActionResult<PropertyTraceDto>> Create(PropertyTraceDto dto)
    //{
    //    var entity = _mapper.Map<PropertyTrace>(dto);
    //    await _propertyTraceRepository.CreateAsync(entity);
    //    return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<PropertyTraceDto>(entity));
    //}
}
