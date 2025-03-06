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

            existingGame.Name = request.Name;
            existingGame.Platform = request.Platform;
            existingGame.Publisher = request.Publisher;
            existingGame.GenreId = request.GenreId;
            existingGame.Price = request.Price;
            existingGame.ReleaseDate = request.ReleaseDate;
            existingGame.Description = request.Description;

            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        })
        .WithName(EndpointNames.UpdateGame)
        .WithParameterValidation();
    }
}
