using FacilityHub.Extensions;
using FacilityHub.Models.DTOs;
using FacilityHub.Models.Request;
using FacilityHub.Models.Response;
using FacilityHub.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

public class StatisticsController : ApiController
{
    private readonly IStatisticService _statService;

    public StatisticsController(IMapper mapper, IStatisticService statService) : base(mapper) =>
        _statService = statService;

    [HttpGet("")]
    [ProducesResponseType(typeof(StatisticsDto), 200)]
    public async Task<IActionResult> Get()
    {
        var userId = User.GetCallerId();
        var stats = await _statService.Get(userId);
        return Ok(stats);
    }
}
