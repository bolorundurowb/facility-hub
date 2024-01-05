using FacilityHub.Services.Interfaces;
using MapsterMapper;

namespace FacilityHub.Controllers;

public class IssuesController : ApiController
{
    private readonly IFacilityService _facilityService;
    private readonly IIssueService _issueService;

    public IssuesController(IMapper mapper, IFacilityService facilityService, IIssueService issueService) : base(mapper)
    {
        _facilityService = facilityService;
        _issueService = issueService;
    }
}
