using System.IdentityModel.Tokens.Jwt;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using VideogamesStore.API.Shared.Authorization;
using VideogamesStore.API.Shared.FileUpload;

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

    public static IServiceCollection AddFileUploader(this IServiceCollection services)
    {
        services.AddSingleton(serviceProvider => 
                {
                    var config = serviceProvider.GetRequiredService<IConfiguration>();
                    var connString = config.GetConnectionString("Blobs");
                    return new BlobServiceClient(connString);
                })
                .AddSingleton<AzureFileUploader>();

        return services;
    }

    public static IHostApplicationBuilder AddGameStoreAuthentication(this IHostApplicationBuilder builder)
    {
        var authBuilder = builder.Services.AddAuthentication(Schemes.KeycloakOrEntra); // Default scheme

        if(builder.Environment.IsDevelopment())
        {
            authBuilder.AddJwtBearer(options => 
            { 
                options.MapInboundClaims = false;
                options.TokenValidationParameters.RoleClaimType = CustomClaimTypes.Role; 
            })
            .AddJwtBearer(Schemes.Keycloak, options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters.RoleClaimType = CustomClaimTypes.Role;
                //options.Events = new JwtBearerEvents { OnTokenValidated = LogJwtBearerEvents() }; // Uncomment if you need to log the claims contained in the token
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context => 
                    {
                        var claimTransformer = context.HttpContext
                                                        .RequestServices
                                                        .GetRequiredService<ClaimsTransformer>();

                        claimTransformer.TransformClaims(context, CustomClaimTypes.Scope, JwtRegisteredClaimNames.Sub);
                        return Task.CompletedTask;
                    }
                };
            });
        }
                
        authBuilder.AddJwtBearer(Schemes.Entra, options => 
        {
            options.MapInboundClaims = false;
            options.TokenValidationParameters.RoleClaimType = CustomClaimTypes.Roles;
            builder.Configuration.GetSection("Authentication:Schemes:Entra").Bind(options);
            options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context => 
                    {
                        var claimTransformer = context.HttpContext
                                                        .RequestServices
                                                        .GetRequiredService<ClaimsTransformer>();

                        claimTransformer.TransformClaims(context, CustomClaimTypes.EntraScope, CustomClaimTypes.EntraOid);
                        return Task.CompletedTask;
                    }
                };
        });

        authBuilder.AddPolicyScheme(Schemes.KeycloakOrEntra, Schemes.KeycloakOrEntra, options => 
        {
            options.ForwardDefaultSelector = context => 
            {
                string authorization = context.Request.Headers[HeaderNames.Authorization]!;

                if(!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                {
                    var token = authorization["Bearer ".Length..].Trim();

                    var jwtHandler = new JwtSecurityTokenHandler();

                    return jwtHandler.CanReadToken(token) && 
                            jwtHandler.ReadJwtToken(token).Issuer.Contains("ciamlogin.com") ? Schemes.Entra : Schemes.Keycloak;
                }

                return Schemes.Entra;
            };
        });

        return builder;
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
