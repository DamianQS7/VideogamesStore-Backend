using System;
using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Games.GetGame.GetGameDtos;

namespace VideogamesStore.API.Features.Games.GetGame;

public static class GetGameMapper
{
    public static GetGameResponse MapToResponse(this Game game) =>
        new(
            game.Id, 
            game.Name,
            game.Platform,
            game.Publisher, 
            game.GenreId,
            game.Price, 
            game.ReleaseDate, 
            game.Description,
            game.ImageUrl
        );
}
