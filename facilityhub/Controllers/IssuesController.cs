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
    private readonly IUserService _userService;
    private readonly IDocumentService _documentService;
    private readonly IMediaHandlerService _mediaService;

    public IssuesController(IMapper mapper, IFacilityService facilityService, IIssueService issueService,
        IUserService userService, IMediaHandlerService mediaService, IDocumentService documentService) : base(mapper)
    {
        _facilityService = facilityService;
        _issueService = issueService;
        _userService = userService;
        _mediaService = mediaService;
        _documentService = documentService;
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

    [HttpGet("{issueId:guid}/documents")]
    [ProducesResponseType(typeof(List<DocumentRes>), 200)]
    public async Task<IActionResult> GetOneDocuments(Guid issueId)
    {
        var userId = User.GetCallerId();
        var documents = await _issueService.GetAllDocuments(userId, issueId);

        return Ok(Mapper.Map<List<DocumentRes>>(documents));
    }

    [HttpPost("{issueId:guid}/documents")]
    [ProducesResponseType(typeof(DocumentRes), 201)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> CreateDocument(Guid issueId, [FromForm] UploadIssueDocumentReq req)
    {
        var userId = User.GetCallerId();
        var user = await _userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await _issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        await using var stream = req.File.OpenReadStream();
        var result = await _mediaService.UploadAsync(req.File.FileName, stream);

        if (result == null)
            return BadRequest("Document upload failed.");

        var document = await _issueService.AddDocument(issue, user, req.Type, result);

        return Created(Mapper.Map<DocumentRes>(document));
    }

    [HttpDelete("{issueId:guid}/documents/{documentId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> DeleteDocument(Guid issueId, Guid documentId)
    {
        var userId = User.GetCallerId();
        var document = await _issueService.FindDocument(userId, issueId, documentId);

        if (document == null)
            return NotFound("Document not found");

        await _mediaService.DeleteAsync(document.ExternalId);
        await _documentService.Delete(document);

        return Ok("Document deleted successfully");
    }

    [HttpPatch("{issueId:guid}/validate")]
    [ProducesResponseType(typeof(IssueRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> ValidateIssue(Guid issueId, [FromBody] IssueStatusChangeReq req)
    {
        var userId = User.GetCallerId();
        var user = await _userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var issue = await _issueService.FindById(userId, issueId);

        if (issue == null)
            return NotFound("Issue not found");

        await _issueService.MarkAsValidated(issue, user, req.Notes);

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
