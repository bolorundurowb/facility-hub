using FacilityHub.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace FacilityHub.Extensions;

public static class ActionContextExtensions
{
    public static ObjectResult Format(this ActionContext actionContext) =>
        new(new ValidationErrorRes(actionContext.ModelState))
        {
            StatusCode = 412
        };
}
