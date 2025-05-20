using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Data;

public static class DataExtensions
{
    private const string PostgreSqlScope = "https://ossrdbms-aad.database.windows.net/.default";
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrateDbAsync(); // Use for development only
        await app.SeedDbAsync();

        app.Logger.LogInformation("Database initialized");
    }

    public static WebApplicationBuilder AddGameStoreNpgsql<TContext>(
        this WebApplicationBuilder builder,
        string connectionStringName,
        TokenCredential credential
    ) where TContext : DbContext
    {
        var connString = builder.Configuration.GetConnectionString(connectionStringName);

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddNpgsql<TContext>(connString);
        }
        else
        {
            builder.Services.AddNpgsql<TContext>(connString, dbContextOptionBuilder =>
            {
                dbContextOptionBuilder.ConfigureDataSource(dataSourceBuilder =>
                {
                    dataSourceBuilder.UsePeriodicPasswordProvider(
                        async (_, cancellationToken) =>
                        {
                            var token = await credential.GetTokenAsync(
                                new TokenRequestContext([PostgreSqlScope]),
                                cancellationToken
                            );

                            return token.Token;
                        },
                        TimeSpan.FromHours(24),
                        TimeSpan.FromSeconds(10)
                    );
                });
            });
        }

        return builder;
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
                new Genre { Name = "Fighting" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Sports" },
                new Genre { Name = "Racing" }
            );
            await dbContext.SaveChangesAsync();
        }
    }
}
