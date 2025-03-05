using System;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Data;

public class GameStoreData
{
    private readonly List<Genre> genres = [
        new Genre { Id = new Guid("9690fe5e-bc81-4e32-bf49-73cf28c634d4"), Name="Fighting" },
        new Genre { Id = new Guid("9210fe5e-bc81-4e32-bf49-73cf28c634d4"), Name="RPG" },
        new Genre { Id = new Guid("4590fe5e-bc81-4e32-bf49-73cf28c634d4"), Name="Sports" },
        new Genre { Id = new Guid("6942fe5e-bc81-4e32-bf49-73cf28c634d4"), Name="Racing" },
    ];

    private readonly List<Game> games;

    public GameStoreData()
    {
        games = 
        [
            new Game {
                Id = Guid.NewGuid(),
                Name = "Final Fantasy IX",
                Genre = genres[1],
                GenreId = genres[1].Id,
                Platform = "Play Station 1",
                Publisher = "Squaresoft",
                Price = 19.99m,
                ReleaseDate = new DateOnly(2000, 7, 7),
                Description = "Best and most underrated final fantasy game of all times."
            },
            new Game {
                Id = Guid.NewGuid(),
                Name = "Final Fantasy VII",
                Genre = genres[1],
                GenreId = genres[1].Id,
                Platform = "Play Station 1",
                Publisher = "Squaresoft",
                Price = 19.99m,
                ReleaseDate = new DateOnly(1997, 1, 31),
                Description = "Very interesting characters."
            },
        ];
    }

    public IEnumerable<Game> GetGames() => games;

    public Game? GetGameById(Guid id) => games.Find(g => g.Id == id);

    public void AddGame(Game game)
    {
        game.Id = Guid.NewGuid();
        games.Add(game);
    }

    public void RemoveGame(Guid id) => games.RemoveAll(g => g.Id == id);

    public IEnumerable<Genre> GetGenres() => genres;
    
    public Genre? GetGenreById(Guid id) => genres.Find(g => g.Id == id);
}
