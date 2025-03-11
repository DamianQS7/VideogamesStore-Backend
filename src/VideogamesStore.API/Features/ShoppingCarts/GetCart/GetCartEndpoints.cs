using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Features.ShoppingCarts.Authorization;
using VideogamesStore.API.Models;
using VideogamesStore.API.Shared.Authorization;
using static VideogamesStore.API.Features.ShoppingCarts.GetCart.GetCartDtos;

namespace VideogamesStore.API.Features.ShoppingCarts.GetCart;

public static class GetCartEndpoint
{
    public static void MapGetCart(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{userId}", async (
            [FromRoute] Guid userId,
            [FromServices] GameStoreContext dbContext,
            [FromServices] IAuthorizationService authorizationService,
            ClaimsPrincipal user) =>
        {
            if (userId == Guid.Empty)
                return Results.BadRequest();

            ShoppingCart cart = await dbContext.ShoppingCarts
                                            .Include(cart => cart.Items)
                                            .ThenInclude(item => item.Game)
                                            .FirstOrDefaultAsync(cart => cart.Id == userId) 
                                            ?? new() { Id = userId };

            var authResult = await authorizationService.AuthorizeAsync(user, cart, new CartOwnerOrAdminRequirement());

            if (!authResult.Succeeded)
                return Results.Forbid();

            GetCartResponse response = cart.MapToResponse();

            return Results.Ok(response);
        })
        .WithName(EndpointNames.GetShoppingCart);
    }
}
