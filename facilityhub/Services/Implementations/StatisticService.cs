using FacilityHub.DataContext;
using FacilityHub.Enums;
using FacilityHub.Models.DTOs;
using FacilityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub.Services.Implementations;

public class StatisticService : IStatisticService
{
    private readonly FacilityHubDbContext _dbContext;

    public StatisticService(FacilityHubDbContext dbContext) => _dbContext = dbContext;

    public async Task<StatisticsDto> Get(Guid userId)
    {
        var facQuery = _dbContext.Facilities.AsNoTracking();
        var issQuery = _dbContext.Issues.AsNoTracking();

        var rented = await facQuery.Where(x => x.Tenant!.User!.Id == userId).CountAsync();
        var owned = await facQuery.Where(x => x.Owners.Any(y => y.Id == userId)).CountAsync();
        var managed = await facQuery.Where(x => x.Managers.Any(y => y.Id == userId)).CountAsync();

        var issuesFiled = await issQuery.Where(x => x.FiledBy!.User!.Id == userId).CountAsync();
        var issuesManaged = await issQuery.Where(x =>
            x.Facility.Managers.Any(y => y.Id == userId) || x.Facility.Owners.Any(y => y.Id == userId)).CountAsync();
        var issuesResolved = await issQuery.Where(x =>
            (x.Status == IssueStatus.Closed || x.Status == IssueStatus.Duplicate) 
            && x.Facility.Managers.Any(y => y.Id == userId) 
            || x.Facility.Owners.Any(y => y.Id == userId))
            .CountAsync();

        return new StatisticsDto(rented, owned, managed, issuesFiled, issuesManaged, issuesResolved);
    }
}
