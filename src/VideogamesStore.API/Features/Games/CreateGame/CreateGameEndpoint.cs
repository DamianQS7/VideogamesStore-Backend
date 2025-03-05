using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapPostGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", (CreateGameRequest gameDto, GameStoreContext dbContext) => 
        {
            Game game = new()
            {
                Id = Guid.NewGuid(),
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                Platform = gameDto.Platform,
                Publisher = gameDto.Publisher,
                ReleaseDate = gameDto.ReleaseDate,
                Description = gameDto.Description
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                EndpointNames.GetGame, 
                new { id = game.Id}, 
                new CreateGameResponse(
                    game.Id,
                    game.Name,
                    game.Publisher,
                    game.Platform,
                    game.GenreId,
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
