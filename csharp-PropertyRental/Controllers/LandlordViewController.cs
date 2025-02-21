using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Data;
using csharp_PropertyRental.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Controllers
{
    public class LandlordViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandlordViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LandlordView/Index (MVC convention)
        public async Task<IActionResult> Index()
        {
            var landlords = await _context.Landlords
                .Select(l => new LandlordViewModel
                {
                    LandlordId = l.LandlordId,
                    LandlordFirstName = l.FirstName,
                    LandlordLastName = l.LastName,
                    Email = l.Email,
                    Phone = l.Phone
                })
                .ToListAsync();

            return View(landlords);
        }

        // GET: LandlordView/AddLandlord (Shows the form)
        public IActionResult AddLandlord()
        {
            return View(new LandlordViewModel());
        }

        // POST: LandlordView/AddLandlord
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLandlord(LandlordViewModel landlordViewModel)
        {
            if (ModelState.IsValid)
            {
                var landlord = new Landlord
                {
                    FirstName = landlordViewModel.LandlordFirstName,
                    LastName = landlordViewModel.LandlordLastName,
                    Email = landlordViewModel.Email,
                    Phone = landlordViewModel.Phone,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Landlords.Add(landlord);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "LandlordView");  // Redirect to the list of landlords
            }
            return View(landlordViewModel); // Return back to the form if validation fails
        }

        // POST: LandlordView/DeleteLandlord/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent CSRF attacks
        public async Task<IActionResult> DeleteLandlord(int id)
        {
            // Find the landlord by ID
            var landlord = await _context.Landlords.FindAsync(id);
            if (landlord == null)
            {
                return NotFound();  // If not found, return 404
            }

            _context.Landlords.Remove(landlord);  // Remove from the database
            await _context.SaveChangesAsync();  // Save changes

            // Redirect to the list view after deletion
            return RedirectToAction(nameof(Index));  // Redirect to the Index view
        }
    }
}
