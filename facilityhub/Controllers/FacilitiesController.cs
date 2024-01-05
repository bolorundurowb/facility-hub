using FacilityHub.Extensions;
using FacilityHub.Models.DTOs;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class FacilitiesController : ApiController
{
    private readonly IFacilityService _facilityService;
    private readonly IUserService _userService;
    private readonly IMediaHandlerService _mediaService;

    public FacilitiesController(IMapper mapper, IFacilityService facilityService, IUserService userService,
        IMediaHandlerService mediaService) :
        base(mapper)
    {
        _facilityService = facilityService;
        _userService = userService;
        _mediaService = mediaService;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<FacilitySummaryDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetCallerId();
        var facilities = await _facilityService.GetAll(userId);

        return Ok(facilities);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(FacilityRes), 201)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    public async Task<IActionResult> Create([FromBody] CreateFacilityReq req)
    {
        var userId = User.GetCallerId();
        var user = await _userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var facility = await _facilityService.Create(user, req.Name, req.Address, req.Location);

        return Created(Mapper.Map<FacilityRes>(facility));
    }

    [HttpGet("{facilityId:guid}")]
    [ProducesResponseType(typeof(FacilityRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetOne(Guid facilityId)
    {
        var userId = User.GetCallerId();
        var facility = await _facilityService.FindById(userId, facilityId);

        if (facility == null)
            return NotFound("Facility not found");

        var response = Mapper.Map<FacilityRes>(facility);
        response.IsTenant = facility.Tenant?.User?.Id == userId;

        return Ok(response);
    }

    [HttpGet("{facilityId:guid}/documents")]
    [ProducesResponseType(typeof(List<DocumentRes>), 200)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> GetDocuments(Guid facilityId)
    {
        var userId = User.GetCallerId();
        var documents = await _facilityService.GetAllDocuments(userId, facilityId);

        return Ok(Mapper.Map<List<DocumentRes>>(documents));
    }

    [HttpPost("{facilityId:guid}/documents")]
    [ProducesResponseType(typeof(DocumentRes), 201)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> CreateDocument(Guid facilityId, [FromForm] UploadDocumentReq req)
    {
        var userId = User.GetCallerId();
        var user = await _userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var facility = await _facilityService.FindById(userId, facilityId);

        if (facility == null)
            return NotFound("Facility not found");

        await using var stream = req.File.OpenReadStream();
        var result = await _mediaService.UploadAsync(req.File.FileName, stream);

        if (result == null)
            return BadRequest("Document upload failed.");

        var document = await _facilityService.AddDocument(facility, user, req.Type, result);

        return Created(Mapper.Map<DocumentRes>(document));
    }
}
