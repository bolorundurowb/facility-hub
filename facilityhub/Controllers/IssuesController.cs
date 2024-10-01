using FacilityHub.Extensions;
using FacilityHub.Models.Data;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class IssuesController(
    IMapper mapper,
    IFacilityService facilityService,
    IIssueService issueService,
    IUserService userService,
    IMediaHandlerService mediaService,
    IDocumentService documentService)
    : ApiController(mapper)
{
    [HttpGet("")]
    [ProducesResponseType(typeof(List<IssueRes>), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetCallerId();
        var issues = await issueService.GetAll(userId);
        return Ok(Mapper.Map<List<IssueRes>>(issues));
    }

    [HttpGet("{issueId:guid}")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetOne(Guid issueId)
    {
        var userId = User.GetCallerId();
        var issue = await issueService.FindById(userId, issueId);

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
        var facility = await facilityService.FindById(userId, req.FacilityId);

        if (facility == null || facility.Tenant?.User?.Id != userId)
            return Forbidden("You cannot report issues on this facility");

        var issue = await issueService.Create(facility, req.OccurredAt, req.Description, req.Location,
            req.RemedialAction);

        return Created(Mapper.Map<IssueRes>(issue));
    }

    [HttpGet("{issueId:guid}/documents")]
    [ProducesResponseType(typeof(List<DocumentRes>), 200)]
    public async Task<IActionResult> GetOneDocuments(Guid issueId)
    {
        var userId = User.GetCallerId();
        var documents = await issueService.GetAllDocuments(userId, issueId);

        return Ok(Mapper.Map<List<DocumentRes>>(documents));
    }

    [HttpPost("{issueId:guid}/documents")]
    [ProducesResponseType(typeof(DocumentRes), 201)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> CreateDocument(Guid issueId, [FromForm] UploadIssueDocumentReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        await using var stream = req.File.OpenReadStream();
        var result = await mediaService.UploadAsync(req.File.FileName, stream);

        if (result == null)
            return BadRequest("Document upload failed.");

        var document = await issueService.AddDocument(issue, user, req.Type, result);

        return Created(Mapper.Map<DocumentRes>(document));
    }

    [HttpDelete("{issueId:guid}/documents/{documentId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> DeleteDocument(Guid issueId, Guid documentId)
    {
        var userId = User.GetCallerId();
        var document = await issueService.FindDocument(userId, issueId, documentId);

        if (document == null)
            return NotFound("Document not found");

        await mediaService.DeleteAsync(document.ExternalId);
        await documentService.Delete(document);

        return Ok("Document deleted successfully");
    }

    [HttpGet("{issueId:guid}/logs")]
    [ProducesResponseType(typeof(List<IssueLogEntry>), 200)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetLogs(Guid issueId)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        var logs = await issueService.GetLogs(userId, issueId);

        return Ok(logs);
    }

    [HttpPatch("{issueId:guid}/validate")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 400)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> ValidateIssue(Guid issueId, [FromBody] IssueStatusChangeReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        if (!issue.CanValidate())
            return BadRequest("Issue cannot be validated");

        await issueService.MarkAsValidated(issue, user, req.Notes);

        return Ok(Mapper.Map<IssueRes>(issue));
    }

    [HttpPatch("{issueId:guid}/schedule-repair")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> ScheduleRepair(Guid issueId, [FromBody] ReadyToRepairReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        await issueService.ScheduleRepair(issue, user, req.Notes, req.RepairerName, req.RepairerPhoneNumber);

        return Ok(Mapper.Map<IssueRes>(issue));
    }

    [HttpPatch("{issueId:guid}/mark-as-duplicate")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 400)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> MarkIssueAsDuplicate(Guid issueId, [FromBody] IssueStatusChangeReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        if (!issue.CanMarkAsDuplicate())
            return BadRequest("Issue cannot be marked as a duplicate");

        await issueService.MarkAsDuplicate(issue, user, req.Notes);

        return Ok(Mapper.Map<IssueRes>(issue));
    }

    [HttpPatch("{issueId:guid}/mark-as-repaired")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 400)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> MarkAsRepaired(Guid issueId, [FromBody] IssueStatusChangeReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        if (!issue.CanMarkAsRepaired())
            return BadRequest("Issue cannot be marked as repaired");

        await issueService.MarkAsRepaired(issue, user, req.Notes);

        return Ok(Mapper.Map<IssueRes>(issue));
    }

    [HttpPatch("{issueId:guid}/close")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 400)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> MarkAsResolved(Guid issueId)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        if (!issue.CanClose(user))
            return BadRequest("Issue cannot be closed/resolved");

        await issueService.MarkAsResolved(issue, user);

        return Ok(Mapper.Map<IssueRes>(issue));
    }
}
