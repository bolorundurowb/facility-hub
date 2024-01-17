using FacilityHub.Enums;
using FacilityHub.Models.Data;
using FacilityHub.Models.DTOs;

namespace FacilityHub.Services.Interfaces;

public interface IFacilityService
{
    Task<List<Facility>> GetAll(Guid userId);

    Task<Facility?> FindById(Guid userId, Guid facilityId);

    Task<Facility> Create(User manager, string name, string address, LocationDto? location);

    Task<List<Document>> GetAllDocuments(Guid userId, Guid facilityId);

    Task<Document?> FindDocument(Guid userId, Guid facilityId, Guid documentId);

    Task<Document> AddDocument(Facility facility, User user, DocumentType documentType,
        IUploadResult details);

    Task DeleteDocument(Facility facility, Document document);

    Task<FacilityInvitation?> FindInvitationById(Guid invitationId);

    Task<Tenant> SetTenant(Facility facility, User inviter, User? user, string emailAddress, DateOnly startsAt,
        DateOnly endsAt, DateOnly paidAt);

    Task InviteContributor(Facility facility, User user, FacilityInvitationType invitationType, string emailAddress);

    Task ClaimInvitation(FacilityInvitation invitation, User user);
}
