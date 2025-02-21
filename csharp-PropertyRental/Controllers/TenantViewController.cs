using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Data;
using csharp_PropertyRental.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Controllers
{
    public class TenantViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TenantView/Index (MVC convention)
        public async Task<IActionResult> Index()
        {
            var tenants = await _context.Tenants
                .Select(t => new TenantViewModel
                {
                    TenantId = t.TenantId,
                    TenantFirstName = t.FirstName,
                    TenantLastName = t.LastName,
                    Email = t.Email,
                    Phone = t.Phone
                })
                .ToListAsync();

            return View(tenants);
        }

        // GET: TenantView/AddTenant (Shows the form)
        public IActionResult AddTenant()
        {
            return View(new TenantViewModel());
        }

        // POST: TenantView/AddTenant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTenant(TenantViewModel tenantViewModel)
        {
            if (ModelState.IsValid)
            {
                var tenant = new Tenant
                {
                    FirstName = tenantViewModel.TenantFirstName,
                    LastName = tenantViewModel.TenantLastName,
                    Email = tenantViewModel.Email,
                    Phone = tenantViewModel.Phone,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Tenants.Add(tenant);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "TenantView");  // Redirect to the list of tenants
            }
            return View(tenantViewModel); // Return back to the form if validation fails
        }

        // POST: TenantView/DeleteTenant/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent CSRF attacks
        public async Task<IActionResult> DeleteTenant(int id)
        {
            // Find the tenant by ID
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();  // If not found, return 404
            }

            _context.Tenants.Remove(tenant);  // Remove from the database
            await _context.SaveChangesAsync();  // Save changes

            // Redirect to the list view after deletion
            return RedirectToAction(nameof(Index));  // Redirect to the Index view
        }

        // Updating
        // GET: TenantView/EditTenant/{id}
        public async Task<IActionResult> EditTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            var viewModel = new TenantViewModel
            {
                TenantId = tenant.TenantId,
                TenantFirstName = tenant.FirstName,
                TenantLastName = tenant.LastName,
                Email = tenant.Email,
                Phone = tenant.Phone
            };

            return View(viewModel);
        }

        // POST: TenantView/EditTenant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTenant(TenantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tenant = await _context.Tenants.FindAsync(model.TenantId);
            if (tenant == null)
            {
                return NotFound();
            }

            // Update tenant properties
            tenant.FirstName = model.TenantFirstName;
            tenant.LastName = model.TenantLastName;
            tenant.Email = model.Email;
            tenant.Phone = model.Phone;
            tenant.UpdatedAt = DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}