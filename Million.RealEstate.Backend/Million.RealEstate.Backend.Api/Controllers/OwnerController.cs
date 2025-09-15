using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Domain.Entities;

namespace Million.RealEstate.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController : ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
    {
        var owners = await _ownerRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<OwnerDto>>(owners));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDto>> GetById(string id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);
        if (owner == null) return NotFound();
        return Ok(_mapper.Map<OwnerDto>(owner));
    }

    [HttpPost]
    public async Task<ActionResult<OwnerDto>> Create(OwnerDto dto)
    {
        var entity = _mapper.Map<Owner>(dto);
        await _ownerRepository.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<OwnerDto>(entity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, OwnerDto dto)
    {
        var entity = _mapper.Map<Owner>(dto);
        entity.Id = id;
        await _ownerRepository.UpdateAsync(id, entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _ownerRepository.DeleteAsync(id);
        return NoContent();
    }
}
