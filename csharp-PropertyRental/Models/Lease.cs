using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRental.Models
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

        public string Terms { get; set; } // Terms

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Date created
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last updated

        // Navigation Properties
        public Property Property { get; set; } // Many-to-One: Lease -> Property
        public Landlord Landlord { get; set; } // Many-to-One: Lease -> Landlord
        public ICollection<LeaseTenant> LeaseTenants { get; set; } // Many-to-Many: Lease -> Tenants
    }
}