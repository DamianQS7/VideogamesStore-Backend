using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Data;

public static class DataExtensions
{
    public static void InitializeDb(this WebApplication app)
    {
        app.MigrateDb(); // Use for development only
        app.SeedDb();
    }

    private static void MigrateDb(this WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();
    }

    private static void SeedDb(this WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if(!dbContext.Genres.Any())
        {
            dbContext.Genres.AddRange(
                new Genre { Name="Fighting" },
                new Genre { Name="RPG" },
                new Genre { Name="Sports" },
                new Genre { Name="Racing" }
            );
            dbContext.SaveChanges();
        }
    }
}
