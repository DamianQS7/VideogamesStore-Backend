using System;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;

namespace VideogamesStore.API.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static void MapDeleteGame(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (Guid id, GameStoreContext dbContext) => 
        {
            await dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        })
        .WithName(EndpointNames.DeleteGame);
    }
}
