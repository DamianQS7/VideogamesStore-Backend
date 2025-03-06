using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Games.CreateGame.CreateGameDtos;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameMapper
{
    public static Game MapToGame(this Request request)
        => new ()
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

    public static Response MapToResponse(this Game game)
        => new (
            game.Id,
            game.Name,
            game.Publisher,
            game.Platform,
            game.GenreId,
            game.Price,
            game.ReleaseDate,
            game.Description
        );
}

