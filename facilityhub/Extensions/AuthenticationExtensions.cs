﻿using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FacilityHub.Extensions;

public static class AuthenticationExtensions
{
    public static Guid GetCallerId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.Identity is not ClaimsIdentity identity)
            return default;

        var id = identity.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        return Guid.Parse(id);
    }
}