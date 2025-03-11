using VideogamesStore.API.Shared.Authorization;

namespace VideogamesStore.API.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public const string ApiAccessScope = "gamestore_api.all";
    public static IServiceCollection AddGameStoreAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
                .AddFallbackPolicy(Policies.UserAccess, builder => builder.RequireClaim("scope", ApiAccessScope))
                .AddPolicy(Policies.AdminAccess, builder => builder.RequireClaim("scope", ApiAccessScope)
                                                                   .RequireRole(Roles.Admin));

        return services;
    }
}
