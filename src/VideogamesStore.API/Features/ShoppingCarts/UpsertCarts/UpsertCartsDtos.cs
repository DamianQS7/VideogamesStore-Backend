namespace VideogamesStore.API.Features.ShoppingCarts.UpsertCarts;

public class UpsertCartsDtos
{
    public record UpsertCartsRequest(IEnumerable<UpsertCartItemRequest> Items);
    public record UpsertCartItemRequest(Guid GameId, int Quantity);
}
