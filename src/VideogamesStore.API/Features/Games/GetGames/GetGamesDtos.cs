namespace VideogamesStore.API.Features.Games.GetGames;

public static class GetGamesDtos
{

    public record GetGamesRequest(int PageNumber = 1, int PageSize = 5, string? Search = null);

    public record GetGamesResponse(
        Guid Id,
        string Name,
        string Platform,
        string Publisher,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate,
        string ImageUrl,
        string Description,
        string LastUpdatedBy
    );

    public record PagedResponse(int TotalPages, IEnumerable<GetGamesResponse> Data);
}
