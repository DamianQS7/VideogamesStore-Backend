using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapPostGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateGameDtos.Request request, GameStoreContext dbContext) => 
        {
            Game game = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                GenreId = request.GenreId,
                Price = request.Price,
                Platform = request.Platform,
                Publisher = request.Publisher,
                ReleaseDate = request.ReleaseDate,
                Description = request.Description
            };

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                EndpointNames.GetGame, 
                new { id = game.Id}, 
                new CreateGameDtos.Response(
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
