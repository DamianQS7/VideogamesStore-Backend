using System.ComponentModel.DataAnnotations;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public record UpdatedGameRequest(
    [Required] string Name,
    [Required] string Platform,
    [Required] string Publisher,
    Guid GenreId,
    [Range(1, 200)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description
);
