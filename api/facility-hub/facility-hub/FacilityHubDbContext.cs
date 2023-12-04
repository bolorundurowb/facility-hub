using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub;

#pragma warning disable CS8618
public class FacilityHubDbContext :DbContext
{
    public DbSet<User> Users { get; private set; }
    
    public FacilityHubDbContext(DbContextOptions<FacilityHubDbContext> options) : base(options) { }
}
