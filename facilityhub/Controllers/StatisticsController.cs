using FacilityHub.Extensions;
using FacilityHub.Models.DTOs;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class StatisticsController(IMapper mapper, IStatisticService statService) : ApiController(mapper)
{
    [HttpGet("")]
    [ProducesResponseType(typeof(StatisticsDto), 200)]
    public async Task<IActionResult> Get()
    {
        var userId = User.GetCallerId();
        var stats = await statService.Get(userId);
        return Ok(stats);
    }
}
