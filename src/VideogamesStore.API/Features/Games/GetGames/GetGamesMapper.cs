using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Games.GetGames.GetGamesDtos;

namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesMapper
{
    public static Response MapToResponse(this Game game)
        => new (
            game.Id,
            game.Name,
            game.Platform,
            game.Publisher,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
}
