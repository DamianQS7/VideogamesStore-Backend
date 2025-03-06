using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapPostGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateGameDtos.Request request, 
                                GameStoreContext dbContext, 
                                ILogger<Program> logger) => 
        {
            Game game = request.MapToGame();

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Created Game: {gameName}", game.Name);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame, 
                new { id = game.Id}, 
                game.MapToResponse());
        })
        .WithName(EndpointNames.PostGame)
        .WithParameterValidation(); // This comes from nuget package MinimalApis.Extensions
    }
}
