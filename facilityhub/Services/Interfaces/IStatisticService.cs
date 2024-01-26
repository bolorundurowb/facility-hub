using FacilityHub.Models.DTOs;

namespace FacilityHub.Services.Interfaces;

public interface IStatisticService
{
    Task<StatisticsDto> Get(Guid userId);
}
