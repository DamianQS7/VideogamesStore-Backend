using System.ComponentModel.DataAnnotations;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameDtos
{
    public record Request(
        [Required] string Name,
        [Required] string Publisher, 
        [Required] string Platform, 
        Guid GenreId,
        [Range(1, 200)] decimal Price,
        DateOnly ReleaseDate,
        [Required] [StringLength(500)] string Description
    );

    public record Response(
        Guid Id, 
        string Name,
        string Publisher,
        string Platform, 
        Guid GenreId, 
        decimal Price, 
        DateOnly ReleaseDate, 
        string Description
    );
}