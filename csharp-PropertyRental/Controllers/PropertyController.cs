using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /Property
    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Add a PropertyDto
        // </summary>
        // <return>
        // 200 OK
        // {PropertyDto}
        // </return>
        // <example>
        // POST: Property/AddProperty -> {PropertyDto}
        // </example>

        [HttpPost("AddProperty")]
        public async Task<ActionResult<PropertyDto>> AddProperty([FromBody] PropertyDto propertyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var property = new Property
            {
                LandlordId = propertyDto.LandlordId,
                Title = propertyDto.Title,
                Description = propertyDto.Description,
                Price = propertyDto.Price,
                Location = propertyDto.Location,
                PropertyType = propertyDto.PropertyType,
                Bedrooms = propertyDto.Bedrooms,
                Bathrooms = propertyDto.Bathrooms,
                SquareFootage = propertyDto.SquareFootage,
                IsAvailable = propertyDto.IsAvailable,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            var createdPropertyDto = new PropertyDto
            {
                PropertyId = property.PropertyId,
                LandlordId = property.LandlordId,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Location = property.Location,
                PropertyType = property.PropertyType,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                SquareFootage = property.SquareFootage,
                IsAvailable = property.IsAvailable
            };

            return Ok(createdPropertyDto);
        }

        // <summary>
        // Returns a list of properties represented by the PropertyDto
        // </summary>
        // <return>
        // 200 OK
        // [{PropertyDto}, {PropertyDto},..]
        // </return>
        // <example>
        // GET: Property/GetAllProperties -> [{PropertyDto, PropertyDto},...]
        // </example>

        [HttpGet("GetAllProperties")]
        public async Task<ActionResult<List<PropertyDto>>> GetAllProperties()
        {
            var properties = await _context.Properties.ToListAsync();
            return Ok(properties);
        }

        // <summary>
        // Returns a single property represented by the PropertyDto
        // </summary>
        // <param name="id">The property id</param>
        // <return>
        // 200 OK
        // {PropertyDto}
        // or 404 Not found
        // </return>
        // <example>
        // GET: Property/GetProperty/{id} -> {PropertyDto}
        // </example>

        [HttpGet("GetProperty/{id}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            var propertyDto = new PropertyDto
            {
                PropertyId = property.PropertyId,
                LandlordId = property.LandlordId,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Location = property.Location,
                PropertyType = property.PropertyType,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                SquareFootage = property.SquareFootage,
                IsAvailable = property.IsAvailable
            };

            return Ok(propertyDto);
        }

        // <summary>
        // DELETE a PropertyDto by ID
        // </summary>
        // <param name="id">The property id</param>
        // <return>
        // 204 No Content
        // or 404 Not found
        // </return>
        // <example>
        // DELETE: Property/DeleteProperty/{id}
        // </example>

        [HttpDelete("DeleteProperty/{id}")]
        public async Task<ActionResult> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // <summary>
        // Update a PropertyDto by ID
        // </summary>
        // <param name="id">The property id</param>
        // <param name="updatedPropertyDto">The updated property data</param>
        // <return>
        // 200 OK
        // {PropertyDto}
        // or 404 Not found
        // </return>
        // <example>
        // PUT: Property/UpdateProperty/{id} -> {PropertyDto}
        // </example>

        [HttpPut("UpdateProperty/{id}")]
        public async Task<ActionResult<PropertyDto>> UpdateProperty(int id, [FromBody] PropertyDto updatedPropertyDto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            property.LandlordId = updatedPropertyDto.LandlordId;
            property.Title = updatedPropertyDto.Title;
            property.Description = updatedPropertyDto.Description;
            property.Price = updatedPropertyDto.Price;
            property.Location = updatedPropertyDto.Location;
            property.PropertyType = updatedPropertyDto.PropertyType;
            property.Bedrooms = updatedPropertyDto.Bedrooms;
            property.Bathrooms = updatedPropertyDto.Bathrooms;
            property.SquareFootage = updatedPropertyDto.SquareFootage;
            property.IsAvailable = updatedPropertyDto.IsAvailable;
            property.UpdatedAt = DateTime.UtcNow;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();

            var propertyDto = new PropertyDto
            {
                PropertyId = property.PropertyId,
                LandlordId = property.LandlordId,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Location = property.Location,
                PropertyType = property.PropertyType,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                SquareFootage = property.SquareFootage,
                IsAvailable = property.IsAvailable
            };

            return Ok(propertyDto);
        }
    }
}