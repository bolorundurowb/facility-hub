using FacilityHub.DataContext;
using FacilityHub.Enums;
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

    public Task<Facility?> FindById(Guid userId, Guid facilityId)
    {
        return _dbContext.Facilities
            .Include(x => x.Documents)
            .Where(x => x.Owner!.Id == userId || x.Managers.Any(y => y.Id == userId))
            .FirstOrDefaultAsync(x => x.Id == facilityId);
    }

    public async Task<Facility> Create(User manager, string name, string address, LocationDto? location)
    {
        var facility = new Facility(name, manager, address, location);
        await _dbContext.Facilities.AddAsync(facility);
        await _dbContext.SaveChangesAsync();

        return facility;
    }

    public async Task<Document> AddDocument(Facility facility, User user, DocumentType documentType,
        IUploadResult details)
    {
        var document = new Document(
            details.FileName,
            details.Size,
            details.Id,
            details.Url,
            details.MimeType,
            user
        );
        facility.Documents.Add(document);
        await _dbContext.SaveChangesAsync();

        return document;
    }
}
