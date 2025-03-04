using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games;
using VideogamesStore.API.Features.Genres;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameStoreData data = new();

app.MapGames(data);
app.MapGenres(data);

app.Run();

