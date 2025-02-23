using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        public int LandlordId { get; set; } // Foreign key 

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public string? PropertyType { get; set; }

        [Required]
        public int Bedrooms { get; set; }

        [Required]
        public int Bathrooms { get; set; }

        public int SquareFootage { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();

        // Navigation Property for Landlord
        public Landlord? Landlord { get; set; }
    }

   public class PropertyDto
    {
        public int PropertyId { get; set; }
        public int LandlordId { get; set; } 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Location { get; set; }
        public string? PropertyType { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareFootage { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}