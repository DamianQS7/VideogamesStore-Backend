using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Games.GetGames.GetGamesDtos;

namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesMapper
{
    public static GetGamesResponse MapToResponse(this Game game, Func<string, string>? cdnUrlTransformer = null)
    {
        cdnUrlTransformer ??= url => url;

        return new(
            game.Id,
            game.Name,
            game.Platform,
            game.Publisher,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate,
            cdnUrlTransformer(game.ImageUrl),
            game.Description,
            cdnUrlTransformer(game.DetailsImageUrl),
            game.LastUpdatedBy
        );
    }

    public static GetGamesResponse MapToResponse(this Game game) =>
        new (
            game.Id,
            game.Name,
            game.Platform,
            game.Publisher,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate,
            game.ImageUrl,
            game.Description,
            game.DetailsImageUrl,
            game.LastUpdatedBy
        );
        
}
