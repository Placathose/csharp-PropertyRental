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

        // Navigation Properties
        public ICollection<Property>? Properties { get; set; } // One-to-Many: Landlord -> Properties

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