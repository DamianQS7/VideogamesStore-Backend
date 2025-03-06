namespace VideogamesStore.API.Features.Genres.GetGenres;

public static class GetGenresDtos
{
    public record Response(Guid Id, string Name);   
}
