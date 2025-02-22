using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class Lease
{
    [Key]
    public int LeaseId { get; set; }

    [Required]
    public int PropertyId { get; set; }

    [Required]
    public int LandlordId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public string? Terms { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public Property? Property { get; set; } // Navigation to Property
    public Landlord? Landlord { get; set; } // Navigation to Landlord
    public ICollection<LeaseTenant> LeaseTenants { get; set; } = new List<LeaseTenant>(); // Many-to-Many with Tenant
}

    public class LeaseDto
    {
        public int LeaseId { get; set; }
        public int PropertyId { get; set; } 
        public int LandlordId { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Terms { get; set; }
    }
}