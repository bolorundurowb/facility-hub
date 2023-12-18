using System.Net;
using FacilityHub.Models.Response;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly IMapper Mapper;

    public ApiController(IMapper mapper) => Mapper = mapper;

    protected BadRequestObjectResult BadRequest(string message) =>
        BadRequest(new GenericRes(message));

    protected NotFoundObjectResult NotFound(string message) =>
        NotFound(new GenericRes(message));

    protected ObjectResult Conflict(string message) =>
        StatusCode((int)HttpStatusCode.Conflict, new GenericRes(message));

    protected CreatedResult Created<T>(T data) => Created(string.Empty, data);

    protected ObjectResult Forbidden(string message) =>
        StatusCode((int)HttpStatusCode.Forbidden, new GenericRes(message));

    protected ObjectResult Error(string message) =>
        StatusCode((int)HttpStatusCode.InternalServerError, new GenericRes(message));
}
