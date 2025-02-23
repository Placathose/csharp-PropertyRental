using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Models;
using csharp_PropertyRental.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace csharp_PropertyRental.Controllers
{
    public class LeaseViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaseViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaseView/Index
        public async Task<IActionResult> Index()
        {
            // Fetch leases with related data
            var leases = await _context.Leases
                .Include(l => l.Property)
                    .ThenInclude(p => p.Landlord) // Include Landlord for each Property
                .Include(l => l.LeaseTenants)
                    .ThenInclude(lt => lt.Tenant) // Include Tenants for each Lease
                .Select(l => new LeaseViewModel
                {
                    LeaseId = l.LeaseId,
                    PropertyId = l.PropertyId,
                    LandlordId = l.LandlordId,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Terms = l.Terms,
                    TenantIds = l.LeaseTenants.Select(t => t.TenantId).ToList()
                })
                .ToListAsync();

            // Fetch additional data for display
            var propertyTitles = await _context.Properties
                .ToDictionaryAsync(p => p.PropertyId, p => p.Title);

            var landlordNames = await _context.Landlords
                .ToDictionaryAsync(l => l.LandlordId, l => $"{l.FirstName} {l.LastName}");

            var tenantNames = await _context.Tenants
                .ToDictionaryAsync(t => t.TenantId, t => $"{t.FirstName} {t.LastName}");

            // Pass additional data to the view using ViewBag
            ViewBag.PropertyTitles = propertyTitles;
            ViewBag.LandlordNames = landlordNames;
            ViewBag.TenantNames = tenantNames;

            return View(leases);
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertiesByLandlord(int landlordId)
        {
            var properties = await _context.Properties
                .Where(p => p.LandlordId == landlordId)
                .Select(p => new SelectListItem
                {
                    Value = p.PropertyId.ToString(),
                    Text = p.Title
                })
                .ToListAsync();

            return Json(properties);
        }

        // GET: LeaseView/AddLease
        public IActionResult AddLease()
        {
            // Fetch all landlords and tenants
            ViewBag.Landlords = _context.Landlords
                .Select(l => new SelectListItem
                {
                    Value = l.LandlordId.ToString(),
                    Text = $"{l.FirstName} {l.LastName}"
                })
                .ToList();

            ViewBag.Tenants = _context.Tenants
                .Select(t => new SelectListItem
                {
                    Value = t.TenantId.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
                })
                .ToList();

            return View(new LeaseViewModel());
        }

        // POST: LeaseView/AddLease
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLease(LeaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns if validation fails
                ViewBag.Properties = _context.Properties.ToList();
                ViewBag.Landlords = _context.Landlords.ToList();
                ViewBag.Tenants = _context.Tenants.ToList();
                return View(model);
            }

            // Create new Lease
            var lease = new Lease
            {
                PropertyId = model.PropertyId,
                LandlordId = model.LandlordId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Terms = model.Terms,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Add Lease to database
            _context.Leases.Add(lease);
            await _context.SaveChangesAsync();

            // Add LeaseTenant records for selected tenants
            foreach (var tenantId in model.TenantIds)
            {
                _context.LeaseTenants.Add(new LeaseTenant
                {
                    LeaseId = lease.LeaseId,
                    TenantId = tenantId
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: LeaseView/EditLease/{id}
        public async Task<IActionResult> EditLease(int id)
        {
            // Fetch the lease by ID
            var lease = await _context.Leases
                .Include(l => l.LeaseTenants)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            // Populate dropdowns
            // ViewBag.Properties = _context.Properties.ToList();
            // ViewBag.Landlords = _context.Landlords.ToList();
            // ViewBag.Tenants = _context.Tenants.ToList();

            // Fetch all landlords and tenants
            ViewBag.Landlords = _context.Landlords
                .Select(l => new SelectListItem
                {
                    Value = l.LandlordId.ToString(),
                    Text = $"{l.FirstName} {l.LastName}"
                })
                .ToList();

            ViewBag.Tenants = _context.Tenants
                .Select(t => new SelectListItem
                {
                    Value = t.TenantId.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
                })
                .ToList();

            // Map to LeaseViewModel
            var model = new LeaseViewModel
            {
                LeaseId = lease.LeaseId,
                PropertyId = lease.PropertyId,
                LandlordId = lease.LandlordId,
                StartDate = lease.StartDate,
                EndDate = lease.EndDate,
                Terms = lease.Terms,
                TenantIds = lease.LeaseTenants.Select(t => t.TenantId).ToList()
            };

            return View(model);
        }

        // POST: LeaseView/EditLease
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLease(LeaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns if validation fails
                ViewBag.Properties = _context.Properties.ToList();
                ViewBag.Landlords = _context.Landlords.ToList();
                ViewBag.Tenants = _context.Tenants.ToList();
                return View(model);
            }

            // Fetch the lease by ID
            var lease = await _context.Leases
                .Include(l => l.LeaseTenants)
                .FirstOrDefaultAsync(l => l.LeaseId == model.LeaseId);

            if (lease == null)
            {
                return NotFound();
            }

            // Update lease properties
            lease.PropertyId = model.PropertyId;
            lease.LandlordId = model.LandlordId;
            lease.StartDate = model.StartDate;
            lease.EndDate = model.EndDate;
            lease.Terms = model.Terms;
            lease.UpdatedAt = DateTime.UtcNow;

            // Update LeaseTenant records
            var existingTenantIds = lease.LeaseTenants.Select(t => t.TenantId).ToList();
            var newTenantIds = model.TenantIds;

            // Remove unselected tenants
            foreach (var tenantId in existingTenantIds.Except(newTenantIds))
            {
                var leaseTenant = await _context.LeaseTenants
                    .FirstOrDefaultAsync(lt => lt.LeaseId == lease.LeaseId && lt.TenantId == tenantId);

                if (leaseTenant != null)
                {
                    _context.LeaseTenants.Remove(leaseTenant);
                }
            }

            // Add new tenants
            foreach (var tenantId in newTenantIds.Except(existingTenantIds))
            {
                _context.LeaseTenants.Add(new LeaseTenant
                {
                    LeaseId = lease.LeaseId,
                    TenantId = tenantId
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: LeaseView/DeleteLease/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLease(int id)
        {
            // Fetch the lease by ID
            var lease = await _context.Leases
                .Include(l => l.LeaseTenants)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null)
            {
                return NotFound();
            }

            // Remove associated LeaseTenant records
            _context.LeaseTenants.RemoveRange(lease.LeaseTenants);

            // Remove the lease
            _context.Leases.Remove(lease);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}