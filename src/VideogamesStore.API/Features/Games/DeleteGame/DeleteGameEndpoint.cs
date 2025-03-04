using System;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;

namespace VideogamesStore.API.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static void MapDeleteGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapDelete("/{id}", (Guid id) => 
        {
            data.RemoveGame(id);

            return Results.NoContent();
        })
        .WithName(EndpointNames.DeleteGame);
    }
}
