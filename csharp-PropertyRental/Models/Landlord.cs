using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class Landlord
    {
        [Key]
        public int LandlordId { get; set; } 

        [Required]
        public string? FirstName { get; set; } 

        [Required]
        public string? LastName { get; set; } 

        [Required]
        [EmailAddress]
        public string? Email { get; set; } 

        [Required]
        public string? Phone { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Date registered
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last updated

        // Navigation Properties for one-to-many relationship with Lease
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();

        // Navigation Property for one-to-many relationship with Property
        public ICollection<Property> Properties { get; set; } = new List<Property>();

    }

    public class LandlordDto
    {
        public int LandlordId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? LandlordFirstName { get; set; } 

        [Required(ErrorMessage = "Last name is required.")]
        public string? LandlordLastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string? Phone { get; set; }

    }
}