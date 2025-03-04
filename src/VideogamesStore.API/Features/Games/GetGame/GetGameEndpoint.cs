using System;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/{id}", (Guid id) => 
        {
            Game? game = data.GetGameById(id);

            return game is null ? Results.NotFound() : Results.Ok(new GetGameResponse(
                game.Id, game.Name, game.Genre.Id, game.Price, game.ReleaseDate, game.Description
            ));
        })
        .WithName(EndpointNames.GetGame); 
    }
}
