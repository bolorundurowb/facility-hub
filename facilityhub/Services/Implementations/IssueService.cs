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
        var facilityIds = await GetAccessibleFacilityIds(userId);
        return await _dbContext.Facilities
            .AsNoTracking()
            .Where(x => facilityIds.Contains(x.Id))
            .SelectMany(x => x.Issues)
            .ToListAsync();
    }

    public Task<List<Issue>> GetAllForFacility(Guid userId, Guid facilityId)
    {
        return _dbContext.Facilities
            .AsNoTracking()
            .Where(x => x.Id == facilityId)
            .Where(x =>
                x.Tenant!.User!.Id == userId
                || x.Owners.Any(y => y.Id == userId)
                || x.Managers.Any(y => y.Id == userId)
            )
            .SelectMany(x => x.Issues)
            .ToListAsync();
    }

    public async Task<Issue?> FindById(Guid userId, Guid issueId)
    {
        var facilityIds = await GetAccessibleFacilityIds(userId);
        return await _dbContext.Facilities
            .AsNoTracking()
            .Where(x => facilityIds.Contains(x.Id))
            .SelectMany(x => x.Issues)
            .FirstOrDefaultAsync(x => x.Id == issueId);
    }

    private Task<List<Guid>> GetAccessibleFacilityIds(Guid userId) => _dbContext.Facilities
        .AsNoTracking()
        .Where(x =>
            x.Tenant!.User!.Id == userId
            || x.Owners.Any(y => y.Id == userId)
            || x.Managers.Any(y => y.Id == userId)
        )
        .Select(x => x.Id)
        .ToListAsync();
}
