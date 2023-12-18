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

    public FacilitiesController(IMapper mapper, IFacilityService facilityService, IUserService userService) :
        base(mapper)
    {
        _facilityService = facilityService;
        _userService = userService;
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
}
