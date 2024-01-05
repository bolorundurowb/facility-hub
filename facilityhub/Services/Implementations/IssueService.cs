using FacilityHub.DataContext;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Services.Implementations;

public class IssueService : IIssueService
{
    private readonly FacilityHubDbContext _dbContext;

    public IssueService(FacilityHubDbContext dbContext) => _dbContext = dbContext;
}
