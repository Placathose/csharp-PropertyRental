using System.ComponentModel.DataAnnotations;
namespace csharp_PropertyRental.Models
{
    public class TenantViewModel
    {
        public int TenantId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string? TenantFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? TenantLastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string? Phone { get; set; }
    }
}