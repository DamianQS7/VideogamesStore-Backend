using VideogamesStore.API.Features.ShoppingCarts.GetCart;
using VideogamesStore.API.Features.ShoppingCarts.UpsertCarts;

namespace VideogamesStore.API.Features.ShoppingCarts;

public static class ShoppingCartsEndpoints
{
    public static void MapShoppingCarts(this WebApplication app)
    {
        var group = app.MapGroup("/shopping-carts");

        group.MapUpsertCart();
        group.MapGetCart();
    }
}
