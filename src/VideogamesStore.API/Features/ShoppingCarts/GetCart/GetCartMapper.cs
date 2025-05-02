using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.ShoppingCarts.GetCart.GetCartDtos;

namespace VideogamesStore.API.Features.ShoppingCarts.GetCart;

public static class GetCartMapper
{
    public static GetCartResponse MapToResponse(this ShoppingCart cart)
    {
        var items = cart.Items
                        .Select(item => item.MapToItemResponse())
                        .OrderBy(item => item.Name);
                        
        decimal totalAmount = items.Sum(item => item.Price * item.Quantity);
        
        return new GetCartResponse(cart.Id, items, totalAmount);
    }

    public static GetCartItemResponse MapToItemResponse(this CartItem item)
        => new(
            item.GameId,
            item.Game!.Name,
            item.Game.Price,
            item.Quantity,
            item.Game.ImageUrl
        );
}
