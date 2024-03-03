using FacilityHub.Enums;
using FacilityHub.Models.Data;

namespace FacilityHub.Services.Interfaces;

public interface IIssueService
{
    Task<List<Issue>> GetAll(Guid userId);

    Task<List<Issue>> GetAllForFacility(Guid userId, Guid facilityId);

    Task<Issue?> FindById(Guid userId, Guid issueId);

    Task<Issue> Create(Facility facility, DateTimeOffset occurredAt, string description,
        string location, string? remedialAction);

    Task<List<Document>> GetAllDocuments(Guid userId, Guid issueId);

    Task<Document?> FindDocument(Guid userId, Guid issueId, Guid documentId);

    Task<Document> AddDocument(Issue issue, User user, DocumentType documentType,
        IUploadResult details);

    Task MarkAsValidated(Issue issue, User manager, string? notes);

    Task ScheduleRepair(Issue issue, User manager, string? notes, string? repairerName, string? repairerPhoneNumber);
}
