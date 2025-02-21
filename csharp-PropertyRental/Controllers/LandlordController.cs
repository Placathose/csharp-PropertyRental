using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;
using Microsoft.EntityFrameworkCore;

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

         // <summary>
        // Add a LandlordDto
        // </summary>
        // <return>
        // 200 OK
        // [{LandlordDto}, {LandlordDto},..]
        // </return>
        // <example>
        // GET: LandlordDto/GetAllLandlords -> [{LandlordDto, LandlordDto},...]
        // </example>

        // POST route to handle adding a landlord
        // Route: /Landlord/AddLandlord
        // Notes: Task<ActionResult<List<LandlordDto>>> accept an array/list
        // Notes: Task<ActionResult<LandlordDto>> accept one landlord

        [HttpPost("AddLandlord")] 
        public async Task<ActionResult<List<LandlordDto>>> AddLandlord([FromBody] LandlordDto landlordDto)
        {
            // Check BodyData against the model (LandlordDto)
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); // Return 400 if validation fails
            }

            var landlord = new Landlord
            {
                FirstName = landlordDto.LandlordFirstName,
                LastName = landlordDto.LandlordLastName,
                Email = landlordDto.Email,
                Phone = landlordDto.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Landlords.Add(landlord); 
            await _context.SaveChangesAsync(); 

            // return the Dto to Control data exposure from API
            var createdLandlordDto = new LandlordDto
            {
                LandlordId = landlord.LandlordId,
                LandlordFirstName = landlord.FirstName,
                LandlordLastName = landlord.LastName,
                Email = landlord.Email,
                Phone = landlord.Phone
            };

            return Ok(landlord); 
        }


        // <summary>
        // Returns a list of landlords represented by the LandlordDto
        // </summary>
        // <return>
        // 200 OK
        // [{LandlordDto}, {LandlordDto},..]
        // </return>
        // <example>
        // GET: LandlordDto/GetAllLandlords -> [{LandlordDto, LandlordDto},...]
        // </example>
        // GET route to retrieve all landlords

         [HttpGet("GetAllLandlords")] 
        public async Task<ActionResult<List<LandlordDto>>> GetAllLandlords()
        {
            // Retrieve all landlords from the database
            var landlords = await _context.Landlords.ToListAsync(); 
            return Ok(landlords); 
        }

         // <summary>
        // Returns a single landlord represented by the LandlordDto
        // </summary>
        //  <param name="id">The landlord id</param>
        // <return>
        // 200 OK
        //  {LandlordDto}
        // or 404 Not found
        // </return>
        // <example>
        // GET: LandlordDto/FindLandlord/{id} -> [{LandlordDto, LandlordDto},...]
        // </example>

        [HttpGet("GetLandlord/{id}")]
        public async Task<ActionResult<LandlordDto>> GetLandlord(int id)
        {
            var landlord = await _context.Landlords.FindAsync(id);
            if(landlord == null)
            {
                return NotFound();
            } else 
            {
                return Ok(landlord);
            }
        }

       // DELETE a LandlordDto by ID
        [HttpDelete("DeleteLandlord/{id}")]
        public async Task<ActionResult> DeleteALandlord(int id)
        {
            var landlord = await _context.Landlords.FindAsync(id);
            if(landlord == null)
            {
                return NotFound();
            }

            _context.Landlords.Remove(landlord);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("UpdateLandlord/{id}")]
        public async Task<ActionResult<LandlordDto>> UpdateLandlord(int id, [FromBody] LandlordDto updatedLandlordDto)
        {
            var landlord = await _context.Landlords.FindAsync(id);
            if(landlord == null)
            {
                return NotFound();
            }

            // Update landlord fields
            landlord.FirstName = updatedLandlordDto.LandlordFirstName;
            landlord.LastName = updatedLandlordDto.LandlordLastName;
            landlord.Email = updatedLandlordDto.Email;
            landlord.Phone = updatedLandlordDto.Phone;
            landlord.UpdatedAt = DateTime.UtcNow;

            _context.Landlords.Update(landlord);

            // save it in db
            await _context.SaveChangesAsync();

            // Return 200 with updated landlord JSON
            return Ok(landlord);
        }
    }
}