using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.ShoppingCarts.UpsertCarts.UpsertCartsDtos;

namespace VideogamesStore.API.Features.ShoppingCarts.UpsertCarts;

public static class UpsertCartsMapper
{
    public static CartItem MapToCartItem(this UpsertCartItemRequest request)
        => new() { GameId = request.GameId, Quantity = request.Quantity };
}
