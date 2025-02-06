using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class LeaseTenant
    {
        [Key]
        public int LeaseTenantId { get; set; }

        [Required]
        public int LeaseId { get; set; }

        [Required]
        public int TenantId { get; set; }

        public Lease? Lease { get; set; }
        public Tenant? Tenant { get; set; }
    }
}