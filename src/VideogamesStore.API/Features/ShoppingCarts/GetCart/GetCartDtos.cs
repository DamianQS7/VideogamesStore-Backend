namespace VideogamesStore.API.Features.ShoppingCarts.GetCart;

public class GetCartDtos
{
    public record GetCartResponse(
        Guid CustomerId, 
        IEnumerable<GetCartItemResponse> Items,
        decimal TotalAmount
    );
    
    public record GetCartItemResponse(
        Guid Id,
        string Name,
        decimal Price,
        int Quantity,
        string ImageUrl
    );
}
