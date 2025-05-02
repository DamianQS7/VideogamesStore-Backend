namespace VideogamesStore.API.Models;

public class Game
{
    public Guid Id { get; set;}
    public required string Name { get; set; }
    public required string Platform { get; set; }
    public Genre? Genre { get; set; }
    public Guid GenreId { get; set; }
    public decimal Price { get; set; }
    public required string Publisher { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required string DetailsImageUrl { get; set; }
    public required string LastUpdatedBy { get; set; }
}
