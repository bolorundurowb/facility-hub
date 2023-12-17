using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub;

public class FacilityHubDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Facility> Facilities => Set<Facility>();

    public DbSet<Issue> Issues => Set<Issue>();

    public FacilityHubDbContext(DbContextOptions<FacilityHubDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<User>()
            .Property(x => x.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Facility>()
            .Property(x => x.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        modelBuilder.Entity<Issue>()
            .Property(x => x.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        base.OnModelCreating(modelBuilder);
    }
}