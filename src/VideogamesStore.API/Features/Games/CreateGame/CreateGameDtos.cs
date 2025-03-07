using System.ComponentModel.DataAnnotations;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameDtos
{
    public record CreateGameRequest(
        [Required] string Name,
        [Required] string Publisher, 
        [Required] string Platform, 
        Guid GenreId,
        [Range(1, 200)] decimal Price,
        DateOnly ReleaseDate,
        [Required] [StringLength(500)] string Description
    )
    {
        public IFormFile? ImageFile { get; set; }
    };

    public record CreateGameResponse(
        Guid Id, 
        string Name,
        string Publisher,
        string Platform, 
        Guid GenreId, 
        decimal Price, 
        DateOnly ReleaseDate, 
        string Description,
        string ImageUrl
    );
}