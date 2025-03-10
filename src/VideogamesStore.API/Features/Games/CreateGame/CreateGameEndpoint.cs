using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;
using VideogamesStore.API.Shared.FileUpload;
using static VideogamesStore.API.Features.Games.CreateGame.CreateGameDtos;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public const string DefaultImageUrl = "https://placehold.co/100";
    public static void MapPostGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromForm] CreateGameRequest request, 
                                GameStoreContext dbContext, 
                                ILogger<Program> logger,
                                FileUploader fileUploader,
                                ClaimsPrincipal user) => 
        {
            if(user?.Identity?.IsAuthenticated == false)
                 return Results.Unauthorized();

            //string? userId = user?.FindFirstValue(ClaimTypes.NameIdentifier); // Old way, requires options in Program.cs
            string? userId = user?.FindFirstValue(JwtRegisteredClaimNames.Sub); // New way

            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            string imageUrl = DefaultImageUrl;

            if(request.ImageFile is not null)
            {
                var fileUploadResult = await fileUploader.UploadFileAsync(
                    request.ImageFile, StorageNames.GameImagesFolder
                );

                if (!fileUploadResult.IsSuccess)
                    return Results.BadRequest(fileUploadResult.ErrorMessage);

                imageUrl = fileUploadResult.FileUrl!;
            }

            Game game = request.MapToGame(imageUrl, userId!);

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Created Game: {gameName}", game.Name);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame, 
                new { id = game.Id}, 
                game.MapToResponse());
        })
        .WithName(EndpointNames.PostGame)
        .WithParameterValidation() // This comes from nuget package MinimalApis.Extensions
        .DisableAntiforgery();
    }
}
