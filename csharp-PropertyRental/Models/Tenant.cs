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

        public ICollection<LeaseTenant>? LeaseTenants { get; set; }
    }
}