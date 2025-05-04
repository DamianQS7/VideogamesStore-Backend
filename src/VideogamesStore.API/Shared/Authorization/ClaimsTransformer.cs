using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VideogamesStore.API.Shared.Extensions;

namespace VideogamesStore.API.Shared.Authorization;

public class ClaimsTransformer(ILogger<ClaimsTransformer> logger)
{
    public void TransformClaims(TokenValidatedContext context, string scopeClaimName, string userIdClaimName)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;

        identity.TransformScopeClaims(scopeClaimName);
        identity?.MapUserIdClaim(userIdClaimName);
        
        context.Principal?.LogAllClaims(logger);
    }
}
