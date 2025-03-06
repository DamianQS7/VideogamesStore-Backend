using VideogamesStore.API.Models;
using static VideogamesStore.API.Features.Genres.GetGenres.GetGenresDtos;

namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresMapper
{
    public static Response MapToResponse(this Genre genre) => new(genre.Id, genre.Name);
    
}
