using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapPutGame(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", (Guid id, UpdatedGameRequest updatedGame, GameStoreContext dbContext) => 
        {
            Game? existingGame = dbContext.Games.Find(id);
            
            if (existingGame is null)
                return Results.NotFound();

            existingGame.Name = updatedGame.Name;
            existingGame.Platform = updatedGame.Platform;
            existingGame.Publisher = updatedGame.Publisher;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.Description = updatedGame.Description;

            dbContext.SaveChanges();
            
            return Results.NoContent();
        })
        .WithName(EndpointNames.UpdateGame)
        .WithParameterValidation();
    }
}
