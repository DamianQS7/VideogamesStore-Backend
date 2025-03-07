namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresDtos
{
    public record GetGenresResponse(Guid Id, string Name);   
}
