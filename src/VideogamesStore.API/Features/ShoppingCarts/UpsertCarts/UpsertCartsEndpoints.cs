using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Features.ShoppingCarts.Authorization;
using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.ShoppingCarts.UpsertCarts.UpsertCartsDtos;

namespace VideogamesStore.API.Features.ShoppingCarts.UpsertCarts;

public static class UpsertCartsEndpoints
{
    public static void MapUpsertCart(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{userId}", async(
            [FromRoute] Guid userId,
            [FromBody] UpsertCartsRequest request,
            [FromServices] GameStoreContext dbContext,
            [FromServices] IAuthorizationService authorizationService,
            ClaimsPrincipal user) => 
        {
            var cart = await dbContext.ShoppingCarts
                                        .Include(cart => cart.Items)
                                        .FirstOrDefaultAsync(cart => cart.Id == userId);

            if (cart is null)
            {
                cart = new ShoppingCart()
                {
                    Id = userId,
                    Items = request.Items.Select(item => item.MapToCartItem()).ToList()
                };

                dbContext.ShoppingCarts.Add(cart);
            }
            else
            {
                cart.Items = request.Items.Select(item => item.MapToCartItem()).ToList();
            }

            var authResult = await authorizationService.AuthorizeAsync(user, cart, new CartOwnerOrAdminRequirement());

            if (!authResult.Succeeded)
                return Results.Forbid();

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName(EndpointNames.UpsertShoppingCart);
    }
}
