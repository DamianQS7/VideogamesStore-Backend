namespace VideogamesStore.API.Features.Games.GetGames;

public record GameSummaryResponse(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
