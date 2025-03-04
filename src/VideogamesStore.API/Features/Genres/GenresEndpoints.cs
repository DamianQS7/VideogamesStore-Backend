using System;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Genres.GetGenres;

namespace VideogamesStore.API.Features.Genres;

public static class GenresEndpoints
{
    public static void MapGenres(this IEndpointRouteBuilder app, GameStoreData data)
    {
        RouteGroupBuilder group = app.MapGroup("/genres");

        // Get genres
        group.MapGetGenres(data);
    }
}
