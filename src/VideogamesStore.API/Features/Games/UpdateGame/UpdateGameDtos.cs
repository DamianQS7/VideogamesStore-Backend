using System.ComponentModel.DataAnnotations;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameDtos
{
    public record UpdateGameRequest(
        [Required] string Name,
        [Required] string Platform,
        [Required] string Publisher,
        Guid GenreId,
        [Range(1, 200)] decimal Price,
        DateOnly ReleaseDate,
        [Required] [StringLength(500)] string Description
    )
    {
        public IFormFile? ImageFile { get; set; }
        public IFormFile? DetailsImageFile { get; set; }
    };
}
