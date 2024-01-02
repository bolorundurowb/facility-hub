using FacilityHub.Enums;
using FacilityHub.Extensions;
using FacilityHub.Models.DTOs;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class InvitationsController : ApiController
{
    private readonly IFacilityService _facilityService;
    private readonly IUserService _userService;

    public InvitationsController(IMapper mapper, IFacilityService facilityService, IUserService userService) :
        base(mapper)
    {
        _facilityService = facilityService;
        _userService = userService;
    }

    [HttpPost("tenant")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> InviteTenant([FromBody] FacilityInvitationReq req)
    {
        var userId = User.GetCallerId();
        var user = await _userService.FindById(userId);

        if (user == null)
            return Forbidden("User account not found");

        var facility = await _facilityService.FindById(userId, req.FacilityId);

        if (facility == null)
            return NotFound("Facility not found");

        await _facilityService.InviteContributor(facility, user, FacilityInvitationType.FacilityTenant,
            req.EmailAddress);

        // TODO: send an email

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("{invitationId:guid}/validate")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(GenericRes), 403)]
    [ProducesResponseType(typeof(GenericRes), 404)]
    public async Task<IActionResult> ValidateInvitation(Guid invitationId, [FromBody] ValidateInvitationReq req)
    {
        var invitation = await _facilityService.FindInvitationById(invitationId);

        if (invitation == null || invitation.ClaimToken != req.ClaimToken || invitation.IsClaimed ||
            invitation.IsExpired())
            return BadRequest("Invalid invitation");

        return Ok("Invitation valid");
    }
}
