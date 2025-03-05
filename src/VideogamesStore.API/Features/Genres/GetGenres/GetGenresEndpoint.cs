using System;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;

namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (GameStoreContext dbContext) => 
            dbContext.Genres
                     .Select(genre => new GetGenreResponse(genre.Id, genre.Name))
                     .AsNoTracking());
    }
}
