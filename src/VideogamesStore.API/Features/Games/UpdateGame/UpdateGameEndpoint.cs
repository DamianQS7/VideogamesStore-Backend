using System;
using System.Security.Cryptography.X509Certificates;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapPutGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapPut("/{id}", (Guid id, UpdatedGameRequest updatedGame) => 
        {
            Game? existingGame = data.GetGameById(id);
            
            if (existingGame is null)
                return Results.NotFound();

            Genre? genre = data.GetGenreById(updatedGame.GenreId);

            if (genre is null)
                return Results.BadRequest("Invalid genre ID");

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.Description = updatedGame.Description;

            return Results.NoContent();
        })
        .WithName(EndpointNames.UpdateGame)
        .WithParameterValidation();
    }
}
