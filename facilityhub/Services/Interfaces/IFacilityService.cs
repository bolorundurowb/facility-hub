using FacilityHub.Models.Data;
using FacilityHub.Models.DTOs;

namespace FacilityHub.Services.Interfaces;

public interface IFacilityService
{
    Task<List<FacilitySummaryDto>> GetAll(Guid userId);

    Task<Facility?> FindById(Guid userId, Guid facilityId);

    Task<Facility> Create(User manager, string name, string address, LocationDto? location);
}
