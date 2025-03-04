using System.ComponentModel.DataAnnotations;
using VideogamesStore.API.Data;
using VideogamesStore.API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameStoreData data = new();

// Endpoint Names
const string GetAllGamesEndpoint = "GetAllGames";
const string GetGameByIdEndpoint = "GetGameById";
const string PostGameEndpoint = "PostGame";
const string UpdateGameEndpoint = "UpdateGame";
const string DeleteGameEndpoint = "DeleteGame";

// GET all games
app.MapGet("/games", () => data.GetGames().Select(game => new GameSummaryResponse(
    game.Id,
    game.Name,
    game.Genre.Name,
    game.Price,
    game.ReleaseDate
)))
.WithName(GetAllGamesEndpoint);

// GET game by ID
app.MapGet("/games/{id}", (Guid id) => 
{
    Game? game = data.GetGameById(id);

    return game is null ? Results.NotFound() : Results.Ok(new GameDetailsResponse(
        game.Id, game.Name, game.Genre.Id, game.Price, game.ReleaseDate, game.Description
    ));
})
.WithName(GetGameByIdEndpoint); // This line is not mandatory, but it is a useful thing to do

// POST game
app.MapPost("/games", (CreateGameRequest gameDto) => 
{
    Genre? genre = data.GetGenreById(gameDto.GenreId);

    if (genre is null)
        return Results.BadRequest("Invalid genre ID");

    Game game = new()
    {
        Id = Guid.NewGuid(),
        Name = gameDto.Name,
        Genre = genre,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
    };

    data.AddGame(game);

    return Results.CreatedAtRoute(
        GetGameByIdEndpoint, 
        new { id = game.Id}, 
        new GameDetailsResponse(
            game.Id,
            game.Name,
            game.Genre.Id,
            game.Price,
            game.ReleaseDate,
            game.Description
        )    
    );
})
.WithName(PostGameEndpoint)
.WithParameterValidation(); // This comes from nuget package MinimalApis.Extensions

// PUT Game
app.MapPut("/games/{id}", (Guid id, UpdatedGameRequest updatedGame) => 
{
    Game? existingGame = data.GetGameById(id);
    
    if (existingGame is null)
        return Results.NotFound();

    Genre? genre = data.GetGenreById(updatedGame.GenreId);

    if (genre is null)
        return Results.BadRequest("Invalid genre ID");

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
    existingGame.Description = updatedGame.Description;

    return Results.NoContent();
})
.WithName(UpdateGameEndpoint)
.WithParameterValidation();

// Delete game
app.MapDelete("/games/{id}", (Guid id) => 
{
    data.RemoveGame(id);

    return Results.NoContent();
})
.WithName(DeleteGameEndpoint);

// Get genre
app.MapGet("/genres", () => data.GetGames().Select(genre => new GenreResponse(
    genre.Id, genre.Name
)));

app.Run();

public record CreateGameRequest(
    [Required]  string Name,
    Guid GenreId,
    [Range(1, 200)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description
);

public record UpdatedGameRequest(
    [Required]  string Name,
    Guid GenreId,
    [Range(1, 200)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description
);

public record GameDetailsResponse(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price, 
    DateOnly ReleaseDate, 
    string Description
);

public record GameSummaryResponse(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

public record GenreResponse(Guid Id, string Name);
