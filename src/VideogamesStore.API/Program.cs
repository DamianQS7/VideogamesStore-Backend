using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games;
using VideogamesStore.API.Features.Genres;
using VideogamesStore.API.Features.ShoppingCarts;
using VideogamesStore.API.Features.ShoppingCarts.Authorization;
using VideogamesStore.API.Shared.ErrorHandling;
using VideogamesStore.API.Shared.Extensions;
using VideogamesStore.API.Shared.FileUpload;

var builder = WebApplication.CreateBuilder(args);
{
    // Database Configuration
    string? connString = builder.Configuration.GetConnectionString("VideogamesStore");
    //builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connString));
    builder.Services.AddSqlite<GameStoreContext>(connString);

    // HttpLogging Configuration
    builder.Services.AddHttpLogging(opt => 
    {
        opt.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
        opt.CombineLogs = true;
    });

    // Error Handling Configuration
    builder.Services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();

    // Open API Support
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Register Authentication
    builder.Services.AddAuthentication()
                    .AddJwtBearer(options => 
                    { 
                        options.MapInboundClaims = false;
                        options.TokenValidationParameters.RoleClaimType = "role"; 
                    });

    builder.Services.AddGameStoreAuthorization();

    builder.Services.AddSingleton<IAuthorizationHandler, CartAuthorizationHandler>();
    
    builder.Services.AddHttpContextAccessor()
                    .AddSingleton<FileUploader>();
}

var app = builder.Build();
{
    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGames();
    app.MapGenres();
    app.MapShoppingCarts();

    app.UseHttpLogging();

    if(app.Environment.IsDevelopment())
        app.UseSwagger();
    else
        app.UseExceptionHandler();

    app.UseStatusCodePages();
    await app.InitializeDbAsync();

    app.Run();
}


