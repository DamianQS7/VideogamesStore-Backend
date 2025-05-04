using System.Security.Claims;
using VideogamesStore.API.Shared.Authorization;

namespace VideogamesStore.API.Shared.Extensions;

public static class ClaimsExtensions
{
    public static void TransformScopeClaims(this ClaimsIdentity? identity, string sourceScopeClaimType)
    {
        var scopeClaim = identity?.FindFirst(sourceScopeClaimType);

        if(scopeClaim is null) 
            return;

        var scopes = scopeClaim.Value.Split(' ');

        identity?.RemoveClaim(scopeClaim);

        identity?.AddClaims(scopes.Select(scope => new Claim(CustomClaimTypes.Scope, scope)));
    }

    public static void LogAllClaims(this ClaimsPrincipal? principal, ILogger logger)
    {
        var claims = principal?.Claims;

        if(claims is null)
            return;

        foreach(var claim in claims)
        {
            logger.LogTrace("Claim: {ClaimType}, Value: {ClaimValue}", claim.Type, claim.Value);
        }
    }

    public static void MapUserIdClaim(this ClaimsIdentity? identity, string sourceClaimType)
    {
        var sourceClaim = identity?.FindFirst(sourceClaimType);

        if(sourceClaim is not null)
            identity?.AddClaim(new Claim(CustomClaimTypes.UserId, sourceClaim.Value));
    }
}
