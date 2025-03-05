using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", (Guid id, GameStoreContext dbContext) => 
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() 
                                : Results.Ok(new GetGameDtos.Response(
                                    game.Id, 
                                    game.Name,
                                    game.Platform,
                                    game.Publisher, 
                                    game.GenreId,
                                    game.Price, 
                                    game.ReleaseDate, 
                                    game.Description
                                ));
        })
        .WithName(EndpointNames.GetGame); 
    }
}
