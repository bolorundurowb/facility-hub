using FacilityHub.Models.Data;

namespace FacilityHub.Services.Interfaces;

public interface IIssueService
{
    Task<List<Issue>> GetAll(Guid userId);


    Task<List<Issue>> GetAllForFacility(Guid userId, Guid facilityId);

    Task<Issue?> FindById(Guid userId, Guid issueId);

    Task<Issue> Create(Facility facility, DateTimeOffset occurredAt, string description,
        string location, string? remedialAction);

    Task<List<Document>> GetAllEvidence(Guid userId, Guid issueId);
}
