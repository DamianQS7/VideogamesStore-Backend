using System;
using VideogamesStore.API.Data;

namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/", () => data.GetGenres().Select(genre => new GetGenreResponse(
            genre.Id, genre.Name
        )));
    }
}
