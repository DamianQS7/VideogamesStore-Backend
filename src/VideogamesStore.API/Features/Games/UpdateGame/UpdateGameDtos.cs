using System.ComponentModel.DataAnnotations;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameDtos
{
    public record Request(
        [Required] string Name,
        [Required] string Platform,
        [Required] string Publisher,
        Guid GenreId,
        [Range(1, 200)] decimal Price,
        DateOnly ReleaseDate,
        [Required] [StringLength(500)] string Description
    );
}
