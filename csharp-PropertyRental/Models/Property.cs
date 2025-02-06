using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        public int LandlordId { get; set; }

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

        public Landlord? Landlord { get; set; }
        public ICollection<Lease>? Leases { get; set; }
    }
}