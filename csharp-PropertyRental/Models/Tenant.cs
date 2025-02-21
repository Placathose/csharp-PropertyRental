using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // One-to-Many: tenant -> leaseTenants
        public ICollection<LeaseTenant>? LeaseTenants { get; set; }
    }

    public class TenantDto
    {
        public int TenantId { get; set; }
        public string? TenantFirstName { get; set; }
        public string? TenantLastName { get; set; }
        
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string? Phone { get; set; }
        public string? RentedPropertyName { get; set; }

    }
}