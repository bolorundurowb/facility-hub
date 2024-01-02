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
            .Where(x => x.Owners.Any(y => y.Id == userId) || x.Managers.Any(y => y.Id == userId))
            .Select(x => new FacilitySummaryDto(x.Id, x.Name, x.Address))
            .ToListAsync();
    }

    public Task<Facility?> FindById(Guid userId, Guid facilityId)
    {
        return _dbContext.Facilities
            .Include(x => x.Documents)
            .Where(x => x.Owners.Any(y => y.Id == userId) || x.Managers.Any(y => y.Id == userId))
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
            documentType,
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

    public Task<FacilityInvitation?> FindInvitationById(Guid invitationId)
    {
        return _dbContext.FacilityInvitations
            .FirstOrDefaultAsync(x => x.Id == invitationId);
    }

    public async Task InviteContributor(Facility facility, User user, FacilityInvitationType invitationType,
        string emailAddress)
    {
        var normalizedEmailAddress = emailAddress.Trim().ToLowerInvariant();
        var invitation = await _dbContext.FacilityInvitations
            .FirstOrDefaultAsync(x =>
                x.Facility.Id == facility.Id
                && x.EmailAddress == normalizedEmailAddress
                && x.Type == invitationType
            );

        if (invitation == null)
        {
            invitation = new FacilityInvitation(facility, user, invitationType, normalizedEmailAddress);
            await _dbContext.FacilityInvitations.AddAsync(invitation);
        }
        else if (invitation.IsClaimed)
        {
            throw new InvalidOperationException();
        }
        else
        {
            invitation.GenerateClaimDetails();
        }

        await _dbContext.SaveChangesAsync();
    }
}
