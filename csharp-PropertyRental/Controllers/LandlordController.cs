using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;

namespace csharp_PropertyRental.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /Landlord
    public class LandlordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandlordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST route to handle adding a landlord
        [HttpPost("AddLandlord")] // Route: /Landlord/AddLandlord
        public IActionResult AddLandlord([FromBody] Landlord landlord)
        {
            if (!ModelState.IsValid) // Validate the model
            {
                return BadRequest(ModelState); // Return 400 if validation fails
            }

            _context.Landlords.Add(landlord); // Add the landlord to the database
            _context.SaveChanges(); // Save changes

            return Ok(landlord); // Return 200 with the created landlord
        }

        // GET route to retrieve all landlords
        [HttpGet("GetAllLandlords")] 
        public IActionResult GetAllLandlords()
        {
            // Retrieve all landlords from the database
            var landlords = _context.Landlords.ToList(); 
            return Ok(landlords); 
        }

        // DELETE a Landlord by ID
        [HttpDelete("DeleteLandlord/{id}")]
        public IActionResult DeleteALandlord(int id)
        {
            var landlord = _context.Landlords.Find(id);
            if(landlord == null)
            {
                return NotFound();
            }

            _context.Landlords.Remove(landlord);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("UpdateLandlord/{id}")]
        public IActionResult UpdateLandlord(int id, [FromBody] Landlord updatedLandlord)
        {
            var landlord = _context.Landlords.Find(id);
            if(landlord == null)
            {
                return NotFound();
            }

            // Update landlord fields
            landlord.FirstName = updatedLandlord.FirstName;
            landlord.LastName = updatedLandlord.LastName;
            landlord.Email = updatedLandlord.Email;
            landlord.Phone = updatedLandlord.Phone;
            landlord.UpdatedAt = DateTime.UtcNow;

            _context.Landlords.Update(landlord);

            // save it in db
            _context.SaveChanges();

            // Return 200 with updated landlord JSON
            return Ok(landlord);

        }
    }
}