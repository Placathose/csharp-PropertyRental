using Microsoft.AspNetCore.Mvc;
using csharp_PropertyRental.Data;
using csharp_PropertyRental.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            // Fetch all landlords from the database
            var landlords = _context.Landlords
                .Select(l => new SelectListItem
                {
                    // Value for the dropdown (LandlordId)
                    Value = l.LandlordId.ToString(), 
                    Text = $"{l.FirstName} {l.LastName}" 
                })
                .ToList();

            // Pass the landlords to the view using ViewBag
            ViewBag.Landlords = landlords;

            return View(new PropertyViewModel());
        }


        // POST: PropertyView/AddProperty
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProperty(PropertyViewModel propertyViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }

                // Return to the form with validation errors
                return View(propertyViewModel);
            }

            // Check if the LandlordId exists in the Landlords table
            var landlordExists = await _context.Landlords.AnyAsync(l => l.LandlordId == propertyViewModel.LandlordId);
            if (!landlordExists)
            {
                ModelState.AddModelError("LandlordId", "The specified Landlord does not exist.");
                return View(propertyViewModel);
            }

            var property = new Property
            {
                LandlordId = propertyViewModel.LandlordId, //assign landlord here
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

            return RedirectToAction("Index", "PropertyView");
        }

        // POST: PropertyView/DeleteProperty/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            // Find the property by ID
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();  
            }

            _context.Properties.Remove(property);  
            await _context.SaveChangesAsync();  

            // Redirect to the list view after deletion
            return RedirectToAction(nameof(Index));  
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