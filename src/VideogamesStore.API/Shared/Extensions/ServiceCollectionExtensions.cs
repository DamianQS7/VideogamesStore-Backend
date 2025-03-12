using Microsoft.AspNetCore.Authentication.JwtBearer;
using VideogamesStore.API.Shared.Authorization;

namespace VideogamesStore.API.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public const string ApiAccessScope = "gamestore_api.all";
    public static IServiceCollection AddGameStoreAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
                .AddFallbackPolicy(Policies.UserAccess, 
                    builder => builder.RequireClaim(CustomClaimTypes.Scope, ApiAccessScope))
                .AddPolicy(Policies.AdminAccess, 
                    builder => builder.RequireClaim(CustomClaimTypes.Scope, ApiAccessScope)
                                      .RequireRole(Roles.Admin));

        return services;
    }

    public static IServiceCollection AddGameStoreAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(Schemes.Keycloak)
                .AddJwtBearer(options => 
                { 
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters.RoleClaimType = CustomClaimTypes.Role; 
                })
                .AddJwtBearer(Schemes.Keycloak, options =>
                {
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters.RoleClaimType = CustomClaimTypes.Role;
                    options.Authority = "http://localhost:8080/realms/gamestore";
                    options.Audience = "gamestore-api";
                    options.RequireHttpsMetadata = false; //dev only
                    //options.Events = new JwtBearerEvents { OnTokenValidated = LogJwtBearerEvents() }; // Uncomment if you need to log the claims contained in the token
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context => 
                        {
                            var claimTransformer = context.HttpContext
                                                          .RequestServices
                                                          .GetRequiredService<KeycloakClaimsTransformer>();

                            claimTransformer.Transform(context);
                            return Task.CompletedTask;
                        }
                    };
                });

        return services;
    }

    private static Func<TokenValidatedContext, Task> LogJwtBearerEvents()
    {
        return context =>
        {
            var claims = context.Principal?.Claims;

            if (claims is null)
                return Task.CompletedTask;

            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

            foreach (var claim in claims)
            {
                logger.LogInformation("Claim: {ClaimType}, Value: {ClaimValue}", claim.Type, claim.Value);
            }

            return Task.CompletedTask;
        };
    }
}
