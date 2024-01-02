using FacilityHub.DataContext.Converters;
using FacilityHub.DataContext.EntityConfigurations;
using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub.DataContext;

public class FacilityHubDbContext : DbContext
{
    public DbSet<Facility> Facilities => Set<Facility>();

    public DbSet<FacilityInvitation> FacilityInvitations => Set<FacilityInvitation>();

    public DbSet<Issue> Issues => Set<Issue>();

    public DbSet<User> Users => Set<User>();

    public FacilityHubDbContext(DbContextOptions<FacilityHubDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FacilityInvitationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new IssueEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TenantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }
}
