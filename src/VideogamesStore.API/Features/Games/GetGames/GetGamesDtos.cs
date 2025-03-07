namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesDtos
{

    public record Request(int PageNumber = 1, int PageSize = 5, string? Search = null);

    public record Response(
        Guid Id,
        string Name,
        string Platform,
        string Publisher,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );

    public record PagedResponse(int TotalPages, IEnumerable<Response> Data);
}
