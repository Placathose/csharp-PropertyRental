using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class LeaseTenant
    {
        [Key]
        public int LeaseTenantId { get; set; } // Primary key for the join table

        [Required]
        public int LeaseId { get; set; } // Foreign key to Lease

        [Required]
        public int TenantId { get; set; } // Foreign key to Tenant

        // Navigation Properties
        public Lease? Lease { get; set; } // Navigation to Lease (not nullable)
        public Tenant? Tenant { get; set; } // Navigation to Tenant (not nullable)
    }

    public class LeaseTenantDto
    {
        public int LeaseTenantId { get; set; }
        public int LeaseId { get; set; }
        public int TenantId { get; set; }
    }
}