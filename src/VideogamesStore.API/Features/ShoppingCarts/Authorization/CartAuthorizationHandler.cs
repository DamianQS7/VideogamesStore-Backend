using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using VideogamesStore.API.Models;
using VideogamesStore.API.Shared.Authorization;

namespace VideogamesStore.API.Features.ShoppingCarts.Authorization;

// Handler to enable resource-based authorization.
public class CartAuthorizationHandler
            : AuthorizationHandler<CartOwnerOrAdminRequirement, ShoppingCart>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, CartOwnerOrAdminRequirement requirement, ShoppingCart resource)
    {
        var currentUserId = context.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrEmpty(currentUserId))
            return Task.CompletedTask;

        if (Guid.Parse(currentUserId) == resource.Id || context.User.IsInRole(Roles.Admin))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

// Just a marker class, to indicate a new Authorization Requirement
public class CartOwnerOrAdminRequirement : IAuthorizationRequirement {}
