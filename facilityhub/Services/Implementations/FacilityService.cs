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

    public Task<List<Facility>> GetAll(Guid userId)
    {
        return _dbContext.Facilities
            .AsNoTracking()
            .Where(x =>
                x.Tenant!.User!.Id == userId
                || x.Owners.Any(y => y.Id == userId)
                || x.Managers.Any(y => y.Id == userId)
            )
            .ToListAsync();
    }

    public Task<Facility?> FindById(Guid userId, Guid facilityId)
    {
        return _dbContext.Facilities
            .Include(x => x.Tenant)
            .Include(x => x.Tenant!.User)
            .Where(x =>
                x.Tenant!.User!.Id == userId
                || x.Owners.Any(y => y.Id == userId)
                || x.Managers.Any(y => y.Id == userId)
            )
            .FirstOrDefaultAsync(x => x.Id == facilityId);
    }

    public async Task<Facility> Create(User manager, string name, string address, LocationDto? location)
    {
        var facility = new Facility(name, manager, address, location);
        await _dbContext.Facilities.AddAsync(facility);
        await _dbContext.SaveChangesAsync();

        return facility;
    }

    public Task<List<Document>> GetAllDocuments(Guid userId, Guid facilityId)
    {
        return _dbContext.Facilities
            .Where(x => x.Id == facilityId)
            .Where(x => x.Owners.Any(y => y.Id == userId) || x.Managers.Any(y => y.Id == userId))
            .SelectMany(x => x.Documents)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
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
            .Include(x => x.Facility)
            .FirstOrDefaultAsync(x => x.Id == invitationId);
    }

    public async Task<Tenant> SetTenant(Facility facility, User inviter, User? user, string emailAddress,
        DateOnly startsAt,
        DateOnly endsAt, DateOnly paidAt)
    {
        var tenant = facility.SetTenant(inviter, user, startsAt, endsAt, paidAt);
        await _dbContext.SaveChangesAsync();

        // if the tenant doesn't have a user account, invite them
        if (user == null)
            await InviteContributor(facility, inviter, FacilityInvitationType.FacilityTenant, emailAddress);

        return tenant;
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

    public async Task ClaimInvitation(FacilityInvitation invitation, User user)
    {
        invitation.Claim();

        var facility = invitation.Facility;
        if (invitation.Type == FacilityInvitationType.FacilityTenant)
        {
            await _dbContext.Entry(facility)
                .Reference(x => x.Tenant)
                .LoadAsync();

            if (facility.Tenant == null)
                throw new ArgumentNullException(nameof(facility.Tenant), "No tenant set on facility to be claimed");

            facility.Tenant!.SetUser(user);
        }
        else
        {
            await _dbContext.Entry(facility)
                .Collection(x => x.Managers)
                .LoadAsync();
            await _dbContext.Entry(facility)
                .Collection(x => x.Owners)
                .LoadAsync();

            if (invitation.Type == FacilityInvitationType.FacilityManager)
                facility.AddManager(user);
            else if (invitation.Type == FacilityInvitationType.FacilityOwner)
                facility.AddOwner(user);
        }

        await _dbContext.SaveChangesAsync();
    }
}
