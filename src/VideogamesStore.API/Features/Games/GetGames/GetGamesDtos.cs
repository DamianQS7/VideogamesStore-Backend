namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesDtos
{
    public record Response(
        Guid Id,
        string Name,
        string Platform,
        string Publisher,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}
