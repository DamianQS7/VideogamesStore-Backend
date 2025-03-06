using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;

namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app) => app.MapGet("/", Handler);

    private static async Task<List<GetGenreResponse>> Handler(GameStoreContext dbContext)
        => await dbContext.Genres
                    .Select(genre => new GetGenreResponse(genre.Id, genre.Name))
                    .AsNoTracking()
                    .ToListAsync();
}
