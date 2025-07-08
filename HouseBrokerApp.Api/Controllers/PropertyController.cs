using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseBrokerApp.Domain.Entities;
using HouseBrokerApp.Infrastructure.Interfaces;

namespace HouseBrokerApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        // 🔍 Anyone can search and view
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertyRepository.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string location, decimal? minPrice, decimal? maxPrice, string? propertyType)
        {
            var results = await _propertyRepository.SearchAsync(location, minPrice, maxPrice, propertyType);
            return Ok(results);
        }

        // 🔐 Only Brokers can CREATE
        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Create(Property property)
        {
            property.Id = Guid.NewGuid();
            var created = await _propertyRepository.AddAsync(property);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // 🔐 Only Brokers can UPDATE
        [Authorize(Roles = "Broker")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Property property)
        {
            if (id != property.Id) return BadRequest();
            await _propertyRepository.UpdateAsync(property);
            return NoContent();
        }

        // 🔐 Only Brokers can DELETE
        [Authorize(Roles = "Broker")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _propertyRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
