using System.ComponentModel.DataAnnotations;
namespace csharp_PropertyRental.Models
{
    public class LeaseViewModel
    {
        public int LeaseId { get; set; }

        [Required(ErrorMessage = "Property is required.")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Landlord is required.")]
        public int LandlordId { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        public string? Terms { get; set; }

        [Required(ErrorMessage = "At least one tenant is required.")]
        public List<int> TenantIds { get; set; } = new List<int>(); // List of selected tenant IDs
    }
}