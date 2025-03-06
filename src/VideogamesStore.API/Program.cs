using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games;
using VideogamesStore.API.Features.Genres;

var builder = WebApplication.CreateBuilder(args);
{
    string? connString = builder.Configuration.GetConnectionString("VideogamesStore");
    //builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connString));
    builder.Services.AddSqlite<GameStoreContext>(connString);
    builder.Services.AddSingleton<GameStoreData>();
    builder.Services.AddHttpLogging(opt => 
    {
        opt.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
        opt.CombineLogs = true;
    });
}

var app = builder.Build();
{
    app.MapGames();
    app.MapGenres();

    app.UseHttpLogging();
    await app.InitializeDbAsync();

    app.Run();
}


