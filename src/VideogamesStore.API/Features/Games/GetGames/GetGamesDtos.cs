namespace VideogamesStore.API.Features.Games.GetGames;

public record GameSummaryResponse(
    Guid Id,
    string Name,
    string Platform,
    string Publisher,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
