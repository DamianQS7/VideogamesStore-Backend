using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;

namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/", () => data.GetGames().Select(game => new GameSummaryResponse(
            game.Id,
            game.Name,
            game.Genre.Name,
            game.Price,
            game.ReleaseDate
        )))
        .WithName(EndpointNames.GetGames);
    }
}
