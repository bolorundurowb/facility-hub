using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub;

public class FacilityHubDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public FacilityHubDbContext(DbContextOptions<FacilityHubDbContext> options) : base(options) { }
}