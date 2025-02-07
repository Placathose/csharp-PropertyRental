using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;

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

        // POST: Add a property
        [HttpPost("AddProperty")] // Route: /Property/AddProperty
        public IActionResult AddProperty([FromBody] Property property)
        {
            if (!ModelState.IsValid) // Validate model
            {
                return BadRequest(ModelState); // Return 400 if invalid
            }

            _context.Properties.Add(property); // Add property
            _context.SaveChanges(); // Save changes

            return Ok(property); // Return 200 with property
        }

        // GET: Retrieve all properties
        [HttpGet("GetAllProperties")] // Route: /Property/GetAllProperties
        public IActionResult GetAllProperties()
        {
            var properties = _context.Properties.ToList(); // Get properties
            return Ok(properties); // Return 200 with data
        }

        // GET: Retrieve property by ID
        [HttpGet("GetProperty/{id}")] // Route: /Property/GetProperty/{id}
        public IActionResult GetProperty(int id)
        {
            var property = _context.Properties.Find(id); // Find property
            if (property == null)
            {
                return NotFound(); // Return 404 if not found
            }

            return Ok(property); // Return 200 with property
        }

        // PUT: Update a property
        [HttpPut("UpdateProperty/{id}")] // Route: /Property/UpdateProperty/{id}
        public IActionResult UpdateProperty(int id, [FromBody] Property updatedProperty)
        {
            var property = _context.Properties.Find(id); // Find property
            if (property == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Update property fields
            property.Title = updatedProperty.Title;
            property.Description = updatedProperty.Description;
            property.Price = updatedProperty.Price;
            property.Location = updatedProperty.Location;
            property.PropertyType = updatedProperty.PropertyType;
            property.Bedrooms = updatedProperty.Bedrooms;
            property.Bathrooms = updatedProperty.Bathrooms;
            property.SquareFootage = updatedProperty.SquareFootage;
            property.IsAvailable = updatedProperty.IsAvailable;
            property.UpdatedAt = DateTime.UtcNow;

            _context.Properties.Update(property); // Update property
            _context.SaveChanges(); // Save changes

            return Ok(property); // Return 200 with updated property
        }

        // DELETE: Delete property by ID
        [HttpDelete("DeleteProperty/{id}")] // Route: /Property/DeleteProperty/{id}
        public IActionResult DeleteProperty(int id)
        {
            var property = _context.Properties.Find(id); // Find property
            if (property == null)
            {
                return NotFound(); // Return 404 if not found
            }

            _context.Properties.Remove(property); // Remove property
            _context.SaveChanges(); // Save changes

            return NoContent(); // Return 204 (no content)
        }
    }
}
