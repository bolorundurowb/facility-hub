using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub;

public class FacilityHubDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Facility> Facilities => Set<Facility>();

    public FacilityHubDbContext(DbContextOptions<FacilityHubDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.Id)
            .HasDefaultValueSql("(UUID())");
        
        modelBuilder.Entity<Facility>()
            .Property(x => x.Id)
            .HasDefaultValueSql("(UUID())");
            
        base.OnModelCreating(modelBuilder);
    }
}