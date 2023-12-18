using FacilityHub.Models.DTOs;

namespace FacilityHub.Models.Request;

public class CreateFacilityReq
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public LocationDto? Location { get; set; }
}
