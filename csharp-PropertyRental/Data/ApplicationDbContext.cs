using csharp_PropertyRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp_PropertyRental.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Move DbSet properties outside the constructor
    public DbSet<Landlord> Landlords { get; set; }
   public DbSet<Property> Properties { get; set; }
    public DbSet<Lease> Leases { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<LeaseTenant> LeaseTenants { get; set; }
}