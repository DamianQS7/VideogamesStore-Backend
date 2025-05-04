using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Games.UpdateGame.UpdateGameDtos;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameMapper
{

    public static void UpdateGame(this Game game, UpdateGameRequest request, string imageUrl, string detailsImageUrl, string userId)
    {
        game.UpdateWithRequest(request);
        game.UpdateImagesUrls(imageUrl, detailsImageUrl);
        game.UpdateLastUpdatedBy(userId);
    }
    
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

    public static void UpdateImagesUrls(this Game game, string imageUrl, string detailsImageUrl)
    {
        game.ImageUrl = imageUrl;
        game.DetailsImageUrl = detailsImageUrl;
    }

    public static void UpdateLastUpdatedBy(this Game game, string userId)
    {
        game.LastUpdatedBy = userId;
    }
}
