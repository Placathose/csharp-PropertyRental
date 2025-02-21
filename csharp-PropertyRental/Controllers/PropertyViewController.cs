using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Data;
using csharp_PropertyRental.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Controllers
{
    public class PropertyViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertyView/Index (MVC convention)
        public async Task<IActionResult> Index()
        {
            var properties = await _context.Properties
                .Select(p => new PropertyViewModel
                {
                    PropertyId = p.PropertyId,
                    LandlordId = p.LandlordId,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Location = p.Location,
                    PropertyType = p.PropertyType,
                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    SquareFootage = p.SquareFootage,
                    IsAvailable = p.IsAvailable
                })
                .ToListAsync();

            return View(properties);
        }

        // GET: PropertyView/AddProperty (Shows the form)
        public IActionResult AddProperty()
        {
            return View(new PropertyViewModel());
        }

        // POST: PropertyView/AddProperty
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProperty(PropertyViewModel propertyViewModel)
        {
            if (ModelState.IsValid)
            {
                var property = new Property
                {
                    LandlordId = propertyViewModel.LandlordId,
                    Title = propertyViewModel.Title,
                    Description = propertyViewModel.Description,
                    Price = propertyViewModel.Price,
                    Location = propertyViewModel.Location,
                    PropertyType = propertyViewModel.PropertyType,
                    Bedrooms = propertyViewModel.Bedrooms,
                    Bathrooms = propertyViewModel.Bathrooms,
                    SquareFootage = propertyViewModel.SquareFootage,
                    IsAvailable = propertyViewModel.IsAvailable,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "PropertyView");  // Redirect to the list of properties
            }
            return View(propertyViewModel); // Return back to the form if validation fails
        }

        // POST: PropertyView/DeleteProperty/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent CSRF attacks
        public async Task<IActionResult> DeleteProperty(int id)
        {
            // Find the property by ID
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();  // If not found, return 404
            }

            _context.Properties.Remove(property);  // Remove from the database
            await _context.SaveChangesAsync();  // Save changes

            // Redirect to the list view after deletion
            return RedirectToAction(nameof(Index));  // Redirect to the Index view
        }

        // Updating
        // GET: PropertyView/EditProperty/{id}
        public async Task<IActionResult> EditProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            var viewModel = new PropertyViewModel
            {
                PropertyId = property.PropertyId,
                LandlordId = property.LandlordId,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Location = property.Location,
                PropertyType = property.PropertyType,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                SquareFootage = property.SquareFootage,
                IsAvailable = property.IsAvailable
            };

            return View(viewModel);
        }

        // POST: PropertyView/EditProperty
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperty(PropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var property = await _context.Properties.FindAsync(model.PropertyId);
            if (property == null)
            {
                return NotFound();
            }

            // Update property properties
            property.LandlordId = model.LandlordId;
            property.Title = model.Title;
            property.Description = model.Description;
            property.Price = model.Price;
            property.Location = model.Location;
            property.PropertyType = model.PropertyType;
            property.Bedrooms = model.Bedrooms;
            property.Bathrooms = model.Bathrooms;
            property.SquareFootage = model.SquareFootage;
            property.IsAvailable = model.IsAvailable;
            property.UpdatedAt = DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}