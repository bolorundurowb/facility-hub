using FacilityHub.Models.Data;
using FacilityHub.Models.DTOs;
using FacilityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacilityHub.Services.Implementations;

public class FacilityService : IFacilityService
{
    private readonly FacilityHubDbContext _dbContext;

    public FacilityService(FacilityHubDbContext dbContext) => _dbContext = dbContext;

    public Task<List<FacilitySummaryDto>> GetAll(Guid userId)
    {
        return _dbContext.Facilities
            .AsNoTracking()
            .Where(x => x.Owner!.Id == userId || x.Managers.Any(y => y.Id == userId))
            .Select(x => new FacilitySummaryDto(x.Id, x.Name, x.Address))
            .ToListAsync();
    }

    public async Task<Facility> Create(User manager, string name, string address, LocationDto? location)
    {
        var facility = new Facility(name, manager, address, location);
        await _dbContext.Facilities.AddAsync(facility);
        await _dbContext.SaveChangesAsync();

        return facility;
    }
}
