using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace VideogamesStore.API.Shared.Authorization;

public class KeycloakClaimsTransformer
{
    public void Transform(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;

        var scopeClaim = identity?.FindFirst(CustomClaimTypes.Scope);

        if (scopeClaim is null)
            return;

        var scopes = scopeClaim.Value.Split();

        identity?.RemoveClaim(scopeClaim);

        identity?.AddClaims(scopes.Select(scope => new Claim(CustomClaimTypes.Scope, scope)));
    }
}
