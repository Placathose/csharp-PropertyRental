using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRental.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; } // Primary Key

        [Required]
        public int LandlordId { get; set; } // Foreign Key to Landlord

        [Required]
        public string? Title { get; set; } // Title

        public string? Description { get; set; } // Description

        [Required]
        public decimal Price { get; set; } // Price

        [Required]
        public string? Location { get; set; } // Location

        [Required]
        public string? PropertyType { get; set; } // Property type

        [Required]
        public int Bedrooms { get; set; } // Bedrooms

        [Required]
        public int Bathrooms { get; set; } // Bathrooms

        public int SquareFootage { get; set; } // Square footage

        public bool IsAvailable { get; set; } = true; // Availability

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Date added
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last updated

        // Navigation Properties
        public Landlord? Landlord { get; set; } // Many-to-One: Property -> Landlord
        public ICollection<Lease>? Leases { get; set; } // One-to-Many: Property -> Leases
    }
}