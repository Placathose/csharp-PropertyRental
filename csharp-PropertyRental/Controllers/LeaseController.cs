using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /Lease
    public class LeaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Add a LeaseDto
        // </summary>
        // <return>
        // 200 OK
        // {LeaseDto}
        // </return>
        // <example>
        // POST: Lease/AddLease -> {LeaseDto}
        // </example>

        [HttpPost("AddLease")]
        public async Task<ActionResult<LeaseDto>> AddLease([FromBody] LeaseDto leaseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lease = new Lease
            {
                PropertyId = leaseDto.PropertyId,
                LandlordId = leaseDto.LandlordId,
                StartDate = leaseDto.StartDate,
                EndDate = leaseDto.EndDate,
                Terms = leaseDto.Terms,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Leases.Add(lease);
            await _context.SaveChangesAsync();

            var createdLeaseDto = new LeaseDto
            {
                LeaseId = lease.LeaseId,
                PropertyId = lease.PropertyId,
                LandlordId = lease.LandlordId,
                StartDate = lease.StartDate,
                EndDate = lease.EndDate,
                Terms = lease.Terms
            };

            return Ok(createdLeaseDto);
        }

        // <summary>
        // Returns a list of leases represented by the LeaseDto
        // </summary>
        // <return>
        // 200 OK
        // [{LeaseDto}, {LeaseDto},..]
        // </return>
        // <example>
        // GET: Lease/GetAllLeases -> [{LeaseDto, LeaseDto},...]
        // </example>

        [HttpGet("GetAllLeases")]
        public async Task<ActionResult<List<LeaseDto>>> GetAllLeases()
        {
            var leases = await _context.Leases.ToListAsync();
            return Ok(leases);
        }

        // <summary>
        // Returns a single lease represented by the LeaseDto
        // </summary>
        // <param name="id">The lease id</param>
        // <return>
        // 200 OK
        // {LeaseDto}
        // or 404 Not found
        // </return>
        // <example>
        // GET: Lease/GetLease/{id} -> {LeaseDto}
        // </example>

        [HttpGet("GetLease/{id}")]
        public async Task<ActionResult<LeaseDto>> GetLease(int id)
        {
            var lease = await _context.Leases.FindAsync(id);
            if (lease == null)
            {
                return NotFound();
            }

            var leaseDto = new LeaseDto
            {
                LeaseId = lease.LeaseId,
                PropertyId = lease.PropertyId,
                LandlordId = lease.LandlordId,
                StartDate = lease.StartDate,
                EndDate = lease.EndDate,
                Terms = lease.Terms
            };

            return Ok(leaseDto);
        }

        // <summary>
        // DELETE a LeaseDto by ID
        // </summary>
        // <param name="id">The lease id</param>
        // <return>
        // 204 No Content
        // or 404 Not found
        // </return>
        // <example>
        // DELETE: Lease/DeleteLease/{id}
        // </example>

        [HttpDelete("DeleteLease/{id}")]
        public async Task<ActionResult> DeleteLease(int id)
        {
            var lease = await _context.Leases.FindAsync(id);
            if (lease == null)
            {
                return NotFound();
            }

            _context.Leases.Remove(lease);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // <summary>
        // Update a LeaseDto by ID
        // </summary>
        // <param name="id">The lease id</param>
        // <param name="updatedLeaseDto">The updated lease data</param>
        // <return>
        // 200 OK
        // {LeaseDto}
        // or 404 Not found
        // </return>
        // <example>
        // PUT: Lease/UpdateLease/{id} -> {LeaseDto}
        // </example>

        [HttpPut("UpdateLease/{id}")]
        public async Task<ActionResult<LeaseDto>> UpdateLease(int id, [FromBody] LeaseDto updatedLeaseDto)
        {
            var lease = await _context.Leases.FindAsync(id);
            if (lease == null)
            {
                return NotFound();
            }

            lease.PropertyId = updatedLeaseDto.PropertyId;
            lease.LandlordId = updatedLeaseDto.LandlordId;
            lease.StartDate = updatedLeaseDto.StartDate;
            lease.EndDate = updatedLeaseDto.EndDate;
            lease.Terms = updatedLeaseDto.Terms;
            lease.UpdatedAt = DateTime.UtcNow;

            _context.Leases.Update(lease);
            await _context.SaveChangesAsync();

            var leaseDto = new LeaseDto
            {
                LeaseId = lease.LeaseId,
                PropertyId = lease.PropertyId,
                LandlordId = lease.LandlordId,
                StartDate = lease.StartDate,
                EndDate = lease.EndDate,
                Terms = lease.Terms
            };

            return Ok(leaseDto);
        }
    }
}