using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.CreateGame;
using VideogamesStore.API.Features.Games.DeleteGame;
using VideogamesStore.API.Features.Games.GetGame;
using VideogamesStore.API.Features.Games.GetGames;
using VideogamesStore.API.Features.Games.UpdateGame;

namespace VideogamesStore.API.Features.Games;

public static class GamesEndpoints
{
    public static void MapGames(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/games");

        // GET all games
        group.MapGetGames();

        // GET game by ID
        group.MapGetGame();

        // POST game
        group.MapPostGame();

        // PUT Game
        group.MapPutGame();

        // Delete game
        group.MapDeleteGame();
    }
}
