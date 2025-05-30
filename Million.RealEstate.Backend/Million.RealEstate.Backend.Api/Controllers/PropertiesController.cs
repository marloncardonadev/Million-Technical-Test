using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Core.DTOs;
using Million.RealEstate.Backend.Core.Interfaces;
using MongoDB.Driver;

namespace Million.RealEstate.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            try
            {
                var properties = await _propertyService.GetProperties();

                if (properties == null || !properties.Any())
                {
                    return NotFound("No se encontraron propiedades");
                }

                return Ok(properties);
            }
            catch (MongoConnectionException ex)
            {
                return StatusCode(503, $"Error de conexión con la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpGet("GetFiltered")]
        public async Task<IActionResult> GetFiltered([FromQuery] PropertyFilterDto filterDto)
        {
            try
            {
                var properties = await _propertyService.GetFilteredAsync(filterDto);

                if (properties == null || !properties.Items.Any())
                {
                    return NotFound("No se encontraron propiedades que coincidan con los filtros.");
                }

                return Ok(properties);
            }
            catch (MongoConnectionException ex)
            {
                return StatusCode(503, $"Error de conexión con la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var property = await _propertyService.GetByIdAsync(id);

                if (property == null)
                {
                    return NotFound($"No se encontró la propiedad con ID {id}");
                }

                return Ok(property);
            }
            catch (MongoConnectionException ex)
            {
                return StatusCode(503, $"Error de conexión con la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}
