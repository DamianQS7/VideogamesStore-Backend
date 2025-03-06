using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;

namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app)
        => app.MapGet("/", Handler).WithName(EndpointNames.GetGames);

    private static async Task<List<GetGamesDtos.Response>>? Handler(GameStoreContext dbContext)
        => await dbContext.Games.Include(game => game.Genre) 
                           .Select(game => new GetGamesDtos.Response(
                               game.Id,
                               game.Name,
                               game.Platform,
                               game.Publisher,
                               game.Genre!.Name,
                               game.Price,
                               game.ReleaseDate
                           ))
                           .AsNoTracking()
                           .ToListAsync();
}
