using Microsoft.AspNetCore.Mvc;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;
using VideogamesStore.API.Shared.CDN;

namespace VideogamesStore.API.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async ([FromRoute] Guid id, 
                                   [FromServices] GameStoreContext dbContext, 
                                   [FromServices] CdnUrlTransformer cdnUrlTransformer)=> 
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() 
                                : Results.Ok(game.MapToResponse());
        })
        .WithName(EndpointNames.GetGame)
        .AllowAnonymous(); 
    }
}
