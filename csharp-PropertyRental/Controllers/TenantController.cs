using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;

namespace csharp_PropertyRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : Controller
    {
         private readonly ApplicationDbContext _context;

         public TenantController(ApplicationDbContext context)
         {
            _context = context;
         }

         [HttpPost("AddTenant")]
         public IActionResult AddTenant([FromBody] Tenant tenant)
         {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tenants.Add(tenant);
            _context.SaveChanges();

            return Ok(tenant);
         }

         // GET: Retrieve all tenants
        [HttpGet("GetAllTenants")] 
        public IActionResult GetAllTenants()
        {
            var tenants = _context.Tenants.ToList(); 
            return Ok(tenants); 
        }

        // GET: Retrieve tenant by ID
        [HttpGet("GetTenant/{id}")]
        public IActionResult GetTenant(int id)
        {
            var tenant = _context.Tenants.Find(id); 
            if (tenant == null)
            {
                return NotFound(); 
            }

            return Ok(tenant); 
        }

        // DELETE: Delete a tenant
        [HttpDelete("DeleteTenant/{id}")]
        public IActionResult DeleteTenant(int id)
        {
            var tenant = _context.Tenants.Find(id); 
            if (tenant == null)
            {
                return NotFound(); 
            }

            _context.Tenants.Remove(tenant);
            _context.SaveChanges(); 

            // signals operation success
            return NoContent(); 
        }

        [HttpPut("UpdateTenant/{id}")] 
        public IActionResult UpdateTenant(int id, [FromBody] Tenant updatedTenant)
        {
            // Calling tenant object from db
            var tenant = _context.Tenants.Find(id); 
            if (tenant == null)
            {
                return NotFound(); 
            }

            // Update tenant fields
            tenant.FirstName = updatedTenant.FirstName;
            tenant.LastName = updatedTenant.LastName;
            tenant.Email = updatedTenant.Email;
            tenant.Phone = updatedTenant.Phone;
            tenant.UpdatedAt = DateTime.UtcNow;

            _context.Tenants.Update(tenant);

            // Save it in db
            _context.SaveChanges();

            // Return 200 with updated tenant JSON
            return Ok(tenant); 
        }
    }
}