using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Controllers
{
    [ApiController]
    [Route("[controller]")] // Route: /Tenant
    public class TenantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // <summary>
        // Add a TenantDto
        // </summary>
        // <return>
        // 200 OK
        // {TenantDto}
        // </return>
        // <example>
        // POST: Tenant/AddTenant -> {TenantDto}
        // </example>

        [HttpPost("AddTenant")]
        public async Task<ActionResult<TenantDto>> AddTenant([FromBody] TenantDto tenantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenant = new Tenant
            {
                FirstName = tenantDto.TenantFirstName,
                LastName = tenantDto.TenantLastName,
                Email = tenantDto.Email,
                Phone = tenantDto.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            var createdTenantDto = new TenantDto
            {
                TenantId = tenant.TenantId,
                TenantFirstName = tenant.FirstName,
                TenantLastName = tenant.LastName,
                Email = tenant.Email,
                Phone = tenant.Phone
            };

            return Ok(createdTenantDto);
        }

        // <summary>
        // Returns a list of tenants represented by the TenantDto
        // </summary>
        // <return>
        // 200 OK
        // [{TenantDto}, {TenantDto},..]
        // </return>
        // <example>
        // GET: Tenant/GetAllTenants -> [{TenantDto, TenantDto},...]
        // </example>

        [HttpGet("GetAllTenants")]
        public async Task<ActionResult<List<TenantDto>>> GetAllTenants()
        {
            var tenants = await _context.Tenants.ToListAsync();
            return Ok(tenants);
        }

        // <summary>
        // Returns a single tenant represented by the TenantDto
        // </summary>
        // <param name="id">The tenant id</param>
        // <return>
        // 200 OK
        // {TenantDto}
        // or 404 Not found
        // </return>
        // <example>
        // GET: Tenant/GetTenant/{id} -> {TenantDto}
        // </example>

        [HttpGet("GetTenant/{id}")]
        public async Task<ActionResult<TenantDto>> GetTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            var tenantDto = new TenantDto
            {
                TenantId = tenant.TenantId,
                TenantFirstName = tenant.FirstName,
                TenantLastName = tenant.LastName,
                Email = tenant.Email,
                Phone = tenant.Phone
            };

            return Ok(tenantDto);
        }

        // <summary>
        // DELETE a TenantDto by ID
        // </summary>
        // <param name="id">The tenant id</param>
        // <return>
        // 204 No Content
        // or 404 Not found
        // </return>
        // <example>
        // DELETE: Tenant/DeleteTenant/{id}
        // </example>

        [HttpDelete("DeleteTenant/{id}")]
        public async Task<ActionResult> DeleteTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // <summary>
        // Update a TenantDto by ID
        // </summary>
        // <param name="id">The tenant id</param>
        // <param name="updatedTenantDto">The updated tenant data</param>
        // <return>
        // 200 OK
        // {TenantDto}
        // or 404 Not found
        // </return>
        // <example>
        // PUT: Tenant/UpdateTenant/{id} -> {TenantDto}
        // </example>

        [HttpPut("UpdateTenant/{id}")]
        public async Task<ActionResult<TenantDto>> UpdateTenant(int id, [FromBody] TenantDto updatedTenantDto)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            tenant.FirstName = updatedTenantDto.TenantFirstName;
            tenant.LastName = updatedTenantDto.TenantLastName;
            tenant.Email = updatedTenantDto.Email;
            tenant.Phone = updatedTenantDto.Phone;
            tenant.UpdatedAt = DateTime.UtcNow;

            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();

            var tenantDto = new TenantDto
            {
                TenantId = tenant.TenantId,
                TenantFirstName = tenant.FirstName,
                TenantLastName = tenant.LastName,
                Email = tenant.Email,
                Phone = tenant.Phone
            };

            return Ok(tenantDto);
        }
    }
}