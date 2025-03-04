using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.CreateGame;
using VideogamesStore.API.Features.Games.DeleteGame;
using VideogamesStore.API.Features.Games.GetGame;
using VideogamesStore.API.Features.Games.GetGames;
using VideogamesStore.API.Features.Games.UpdateGame;

namespace VideogamesStore.API.Features.Games;

public static class GamesEndpoints
{
    public static void MapGames(this IEndpointRouteBuilder app, GameStoreData data)
    {
        RouteGroupBuilder group = app.MapGroup("/games");

        // GET all games
        group.MapGetGames(data);

        // GET game by ID
        group.MapGetGame(data);

        // POST game
        group.MapPostGame(data);

        // PUT Game
        group.MapPutGame(data);

        // Delete game
        group.MapDeleteGame(data);
    }
}
