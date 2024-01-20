using FacilityHub.DataContext;
using FacilityHub.Models.Data;
using FacilityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub.Services.Implementations;

public class IssueService : IIssueService
{
    private readonly FacilityHubDbContext _dbContext;

    public IssueService(FacilityHubDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Issue>> GetAll(Guid userId)
    {
        var managedFacilityIds = await GetManagedFacilityIds(userId);
        return await _dbContext.Issues
            .AsNoTracking()
            .Where(x =>
                // you are referenced in the issue
                x.FiledBy.User!.Id == userId
                // or you manage the facility the report is on
                || managedFacilityIds.Contains(x.Facility.Id)
            )
            .ToListAsync();
    }

    public async Task<List<Issue>> GetAllForFacility(Guid userId, Guid facilityId)
    {
        var managedFacilityIds = await GetManagedFacilityIds(userId);
        return await _dbContext.Issues
            .AsNoTracking()
            .Where(x => x.Facility.Id == facilityId)
            .Where(x =>
                // you are referenced in the issue
                x.FiledBy.User!.Id == userId
                // or you manage the facility the report is on
                || managedFacilityIds.Contains(x.Facility.Id)
            )
            .ToListAsync();
    }

    public async Task<Issue?> FindById(Guid userId, Guid issueId)
    {
        var managedFacilityIds = await GetManagedFacilityIds(userId);
        return await _dbContext.Issues
            .Where(x =>
                // you are referenced in the issue
                x.FiledBy.User!.Id == userId
                // or you manage the facility the report is on
                || managedFacilityIds.Contains(x.Facility.Id)
            )
            .FirstOrDefaultAsync(x => x.Id == issueId);
    }

    // public async Task<Issue> Create(Tenant tenant, )

    private Task<List<Guid>> GetManagedFacilityIds(Guid userId) => _dbContext.Facilities
        .AsNoTracking()
        .Where(x => x.Owners.Any(y => y.Id == userId)
                    || x.Managers.Any(y => y.Id == userId)
        )
        .Select(x => x.Id)
        .ToListAsync();
}
