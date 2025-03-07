using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using static VideogamesStore.API.Features.Genres.GetGenres.GetGenresDtos;

namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app) {
        app.MapGet("/", async (GameStoreContext dbContext)
            => await dbContext.Genres
                    .Select(genre => genre.MapToResponse())
                    .AsNoTracking()
                    .ToListAsync());
    }
    // private static async Task<List<Response>> Handler(GameStoreContext dbContext)
    //     => await dbContext.Genres
    //                 .Select(genre => genre.MapToResponse())
    //                 .AsNoTracking()
    //                 .ToListAsync();
}
