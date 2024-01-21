using FacilityHub.Extensions;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("")]
    [ProducesResponseType(typeof(List<IssueRes>), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetCallerId();
        var issues = await _issueService.GetAll(userId);
        return Ok(Mapper.Map<List<IssueRes>>(issues));
    }

    [HttpGet("{issueId:guid}")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetOne(Guid issueId)
    {
        var userId = User.GetCallerId();
        var issue = await _issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        return Ok(Mapper.Map<IssueRes>(issue));
    }

    [HttpPost("report")]
    [ProducesResponseType(typeof(IssueRes), 201)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> Report([FromBody] ReportIssueReq req)
    {
        var userId = User.GetCallerId();
        var facility = await _facilityService.FindById(userId, req.FacilityId);

        if (facility == null || facility.Tenant?.User?.Id != userId)
            return Forbidden("You cannot report issues on this facility");

        var issue = await _issueService.Create(facility, req.OccurredAt, req.Description, req.Location,
            req.RemedialAction);

        return Created(Mapper.Map<IssueRes>(issue));
    }
}
