using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyRental.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; } // Primary Key

        [Required]
        public string? FirstName { get; set; } // First name

        [Required]
        public string? LastName { get; set; } // Last name

        [Required]
        [EmailAddress]
        public string? Email { get; set; } // Email

        [Required]
        public string? Phone { get; set; } // Phone

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Date registered
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last updated

        // Navigation Properties
        public ICollection<LeaseTenant>? LeaseTenants { get; set; } // Many-to-Many: Tenant -> Leases
    }
}