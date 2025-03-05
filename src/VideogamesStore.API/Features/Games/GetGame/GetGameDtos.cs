namespace VideogamesStore.API.Features.Games.GetGame;

public static class GetGameDtos 
{
    public record Response(
        Guid Id, 
        string Name,
        string Platform,
        string Publisher, 
        Guid GenreId, 
        decimal Price, 
        DateOnly ReleaseDate, 
        string Description
    );
}
