using System.Security.Claims;

namespace FacilityHub.Extensions;

public static class AuthenticationExtensions
{
    public static Guid GetCallerId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.Identity is not ClaimsIdentity identity)
            return default;

        var id = identity.Claims.First(x => x.Type == "user").Value;
        return Guid.Parse(id);
    }
}
