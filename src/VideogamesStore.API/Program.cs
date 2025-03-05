using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games;
using VideogamesStore.API.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

string? connString = builder.Configuration.GetConnectionString("VideogamesStore");

//builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connString));
builder.Services.AddSqlite<GameStoreContext>(connString);
builder.Services.AddSingleton<GameStoreData>();

var app = builder.Build();

GameStoreData data = new();

app.MapGames();
app.MapGenres();

app.InitializeDb();

app.Run();

