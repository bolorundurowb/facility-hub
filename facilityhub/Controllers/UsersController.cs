using FacilityHub.Extensions;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class UsersController(IMapper mapper, IUserService userService) : ApiController(mapper)
{
    [HttpGet("current")]
    [ProducesResponseType(typeof(UserRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 401)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Unauthorized("User account does not exist");

        return Ok(Mapper.Map<UserRes>(user));
    }

    [HttpPut("current")]
    [ProducesResponseType(typeof(UserRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 401)]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateProfileReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Unauthorized("User account does not exist");

        user = await userService.Update(user, req.FirstName, req.LastName, req.PhoneNumber);

        return Ok(Mapper.Map<UserRes>(user));
    }

    [HttpPut("current/password")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(GenericRes), 400)]
    [ProducesResponseType(typeof(GenericRes), 401)]
    public async Task<IActionResult> UpdateCurrentUserPassword([FromBody] UpdatePasswordReq req)
    {
        var userId = User.GetCallerId();
        var user = await userService.FindById(userId);

        if (user == null)
            return Unauthorized("User account does not exist");

        if (!user.VerifyPassword(req.CurrentPassword))
            return BadRequest("Invalid current password");

        await userService.UpdatePassword(user, req.Password);

        return NoContent();
    }
}
