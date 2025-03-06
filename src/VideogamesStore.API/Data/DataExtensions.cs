using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrateDbAsync(); // Use for development only
        await app.SeedDbAsync();

        app.Logger.LogInformation("Database initialized");
    }

    private static async Task MigrateDbAsync(this WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await dbContext.Database.MigrateAsync();
    }

    private static async Task SeedDbAsync(this WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if(!dbContext.Genres.Any())
        {
            await dbContext.Genres.AddRangeAsync(
                new Genre { Name="Fighting" },
                new Genre { Name="RPG" },
                new Genre { Name="Sports" },
                new Genre { Name="Racing" }
            );
            await dbContext.SaveChangesAsync();
        }
    }
}
