using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapPostGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapPost("/", (CreateGameRequest gameDto) => 
        {
            Genre? genre = data.GetGenreById(gameDto.GenreId);

            if (genre is null)
                return Results.BadRequest("Invalid genre ID");

            Game game = new()
            {
                Id = Guid.NewGuid(),
                Name = gameDto.Name,
                Genre = genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                Description = gameDto.Description
            };

            data.AddGame(game);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame, 
                new { id = game.Id}, 
                new CreateGameResponse(
                    game.Id,
                    game.Name,
                    game.Genre.Id,
                    game.Price,
                    game.ReleaseDate,
                    game.Description
                )    
            );
        })
        .WithName(EndpointNames.PostGame)
        .WithParameterValidation(); // This comes from nuget package MinimalApis.Extensions
    }
}
