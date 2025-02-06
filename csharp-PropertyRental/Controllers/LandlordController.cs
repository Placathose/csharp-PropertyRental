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
    }
}