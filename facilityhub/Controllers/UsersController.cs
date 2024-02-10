using FacilityHub.Extensions;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IMapper mapper, IUserService userService) : base(mapper) => _userService = userService;

    [HttpGet("current")]
    [ProducesResponseType(typeof(UserRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 401)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.GetCallerId();
        var user = await _userService.FindById(userId);

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
        var user = await _userService.FindById(userId);

        if (user == null)
            return Unauthorized("User account does not exist");

        user = await _userService.Update(user, req.FirstName, req.LastName, req.PhoneNumber);

        return Ok(Mapper.Map<UserRes>(user));
    }
}
