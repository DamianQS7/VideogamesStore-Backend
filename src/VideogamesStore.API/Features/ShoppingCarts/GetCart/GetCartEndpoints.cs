using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.ShoppingCarts.GetCart.GetCartDtos;

namespace VideogamesStore.API.Features.ShoppingCarts.GetCart;

public static class GetCartEndpoint
{
    public static void MapGetCart(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{userId}", async (
            Guid userId,
            GameStoreContext dbContext) =>
        {
            if (userId == Guid.Empty)
                return Results.BadRequest();

            ShoppingCart cart = await dbContext.ShoppingCarts
                                            .Include(cart => cart.Items)
                                            .ThenInclude(item => item.Game)
                                            .FirstOrDefaultAsync(cart => cart.Id == userId) 
                                            ?? new() { Id = userId };

            GetCartResponse response = cart.MapToResponse();

            return Results.Ok(response);
        });
    }
}
