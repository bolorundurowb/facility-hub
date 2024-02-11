using FacilityHub.DataContext;
using FacilityHub.Models.Data;
using FacilityHub.Models.Response;
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
            .Include(x => x.Facility)
            .Include(x => x.FiledBy)
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
            .Include(x => x.Facility)
            .Include(x => x.FiledBy)
            .Where(x =>
                // you are referenced in the issue
                x.FiledBy.User!.Id == userId
                // or you manage the facility the report is on
                || managedFacilityIds.Contains(x.Facility.Id)
            )
            .FirstOrDefaultAsync(x => x.Id == issueId);
    }

    public async Task<Issue> Create(Facility facility, DateTimeOffset occurredAt, string description,
        string location, string? remedialAction)
    {
        var issue = facility.ReportIssue(occurredAt, description, location, remedialAction);
        await _dbContext.SaveChangesAsync();

        return issue;
    }

    public async Task<List<Document>> GetAllDocuments(Guid userId, Guid issueId)
    {
        var managedFacilityIds = await GetManagedFacilityIds(userId);
        return await _dbContext.Issues
            .Where(x =>
                // you are referenced in the issue
                x.FiledBy.User!.Id == userId
                // or you manage the facility the report is on
                || managedFacilityIds.Contains(x.Facility.Id)
            )
            .Where(x => x.Id == issueId)
            .SelectMany(x => x.Documents)
            .ToListAsync();
    }

    public async Task MarkAsValidated(Issue issue, User manager, string? notes)
    {
        issue.Validate(manager, notes);
        await _dbContext.SaveChangesAsync();
    }

    #region Private Helpers

    private Task<List<Guid>> GetManagedFacilityIds(Guid userId) => _dbContext.Facilities
        .AsNoTracking()
        .Where(x => x.Owners.Any(y => y.Id == userId)
                    || x.Managers.Any(y => y.Id == userId)
        )
        .Select(x => x.Id)
        .ToListAsync();

    #endregion
}
