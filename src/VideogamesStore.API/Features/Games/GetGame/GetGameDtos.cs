namespace VideogamesStore.API.Features.Games.GetGame;

public record GetGameResponse(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price, 
    DateOnly ReleaseDate, 
    string Description
);
