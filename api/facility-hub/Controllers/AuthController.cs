using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FacilityHub.Models.Data;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FacilityHub.Controllers;

public class AuthController : ApiController
{
    private readonly IUserService _userService;

    public AuthController(IMapper mapper, IUserService userService): base(mapper) => _userService = userService;
    
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthRes), 200)]
    [ProducesResponseType(typeof(GenericRes), 400)]
    public async Task<IActionResult> Login([FromBody] LoginReq req)
    {
        var user = await _userService.FindByEmail(req.EmailAddress);

        if (user == null)
            return NotFound("Admin account not found.");

        if (!user.VerifyPassword(req.Password))
            return BadRequest("Incorrect password.");

        var (token, expiry) = GenerateToken(user);

        return Ok(new AuthRes(token, expiry, Mapper.Map<UserRes>(user)));
    }
    
    private (string, DateTime) GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var expiry = DateTimeOffset.UtcNow.AddDays(7);
        var token = new JwtSecurityToken
        (
            Config.Issuer,
            Config.Audience,
            claims,
            expires: expiry.UtcDateTime,
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.Secret)),
                SecurityAlgorithms.HmacSha256)
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiry.UtcDateTime);
    }
}
