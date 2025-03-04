using System.ComponentModel.DataAnnotations;

namespace VideogamesStore.API.Features.Games.CreateGame;

public record CreateGameRequest(
    [Required]  string Name,
    Guid GenreId,
    [Range(1, 200)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description
);

public record CreateGameResponse(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price, 
    DateOnly ReleaseDate, 
    string Description
);