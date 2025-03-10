using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using static VideogamesStore.API.Features.Games.GetGames.GetGamesDtos;

namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app)
        => app.MapGet("/", Handler).WithName(EndpointNames.GetGames);

    private static async Task<PagedResponse>? Handler(
        [FromServices] GameStoreContext dbContext, 
        [AsParameters] GetGamesRequest request)
    {
        var games = await dbContext.Games
                            .Where(game => string.IsNullOrWhiteSpace(request.Search) 
                                || EF.Functions.Like(game.Name, $"%{request.Search}%"))
                            .OrderBy(game => game.Name)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Include(game => game.Genre) 
                            .Select(game => game.MapToResponse())
                            .AsNoTracking()
                            .ToListAsync();

        int totalCount = await dbContext.Games.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);
        
        return new PagedResponse(totalPages, games);
    }
        
}
