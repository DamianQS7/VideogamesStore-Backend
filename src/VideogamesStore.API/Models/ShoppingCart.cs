namespace VideogamesStore.API.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public List<CartItem> Items { get; set; } = [];
}
