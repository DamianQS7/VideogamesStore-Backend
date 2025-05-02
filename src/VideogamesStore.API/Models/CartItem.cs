namespace VideogamesStore.API.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public int Quantity { get; set; }
    public Guid ShoppingCartId { get; set; }
}
