using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class Lease
    {
        [Key]
        public int LeaseId { get; set; } // Primary Key

        [Required]
        public int PropertyId { get; set; } // Foreign Key to Property

        [Required]
        public int LandlordId { get; set; } // Foreign Key to Landlord

        [Required]
        public DateTime StartDate { get; set; } // Start date

        [Required]
        public DateTime EndDate { get; set; } // End date

        public string? Terms { get; set; } // Terms

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Date created
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last updated

        // Navigation Properties
        public ICollection<Tenant>? Tenants { get; set; } // One-to-Many: Landlord -> 
    }
}