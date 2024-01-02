using FacilityHub.Enums;
using FacilityHub.Models.Data;
using FacilityHub.Models.DTOs;

namespace FacilityHub.Services.Interfaces;

public interface IFacilityService
{
    Task<List<FacilitySummaryDto>> GetAll(Guid userId);

    Task<Facility?> FindById(Guid userId, Guid facilityId);

    Task<Facility> Create(User manager, string name, string address, LocationDto? location);

    Task<Document> AddDocument(Facility facility, User user, DocumentType documentType,
        IUploadResult details);

    Task InviteContributor(Facility facility, User user, FacilityInvitationType invitationType, string emailAddress);
}
