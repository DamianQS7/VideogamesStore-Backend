using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Games.UpdateGame.UpdateGameDtos;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameMapper
{
    public static void UpdateWithRequest(this Game game, UpdateGameRequest request)
    {
        game.Name = request.Name;
        game.Platform = request.Platform;
        game.Publisher = request.Publisher;
        game.GenreId = request.GenreId;
        game.Price = request.Price;
        game.ReleaseDate = request.ReleaseDate;
        game.Description = request.Description;
    }

    public static void UpdateImageUrl(this Game game, string imageUrl)
    {
        game.ImageUrl = imageUrl;
    }
}
