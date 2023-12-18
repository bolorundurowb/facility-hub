using FacilityHub.Services.Interfaces;
using MapsterMapper;

namespace FacilityHub.Controllers;

public class FacilitiesController : ApiController
{
    private readonly IFacilityService _facilityService;

    public FacilitiesController(IMapper mapper, IFacilityService facilityService) : base(mapper)
    {
        _facilityService = facilityService;
    }
}
