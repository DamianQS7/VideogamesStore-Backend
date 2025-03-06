using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapPutGame(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (Guid id, UpdateGameDtos.Request request, GameStoreContext dbContext) => 
        {
            Game? existingGame = await dbContext.Games.FindAsync(id);
            
            if (existingGame is null)
                return Results.NotFound();

            existingGame.UpdateWithRequest(request);

            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        })
        .WithName(EndpointNames.UpdateGame)
        .WithParameterValidation();
    }
}
