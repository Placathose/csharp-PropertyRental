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
    }
}
