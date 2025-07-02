using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;
using VideogamesStore.API.Shared.Authorization;
using VideogamesStore.API.Shared.CDN;
using VideogamesStore.API.Shared.FileUpload;
using static VideogamesStore.API.Features.Games.CreateGame.CreateGameDtos;

namespace VideogamesStore.API.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public const string DefaultImageUrl = "https://placehold.co/100";
    public static void MapPostGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromForm] CreateGameRequest request, 
                                [FromServices] GameStoreContext dbContext, 
                                [FromServices] ILogger<Program> logger,
                                [FromServices] AzureFileUploader fileUploader,
                                [FromServices] CdnUrlTransformer cdnUrlTransformer,
                                ClaimsPrincipal user) =>
        {
            if (user?.Identity?.IsAuthenticated == false)
                return Results.Unauthorized();


            string? userId = user?.FindFirstValue(JwtRegisteredClaimNames.Email) ??
                             user?.FindFirstValue(CustomClaimTypes.UserId);

            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            string imageUrl;
            string detailsImgUrl;

            try
            {
                imageUrl = await fileUploader.TryUploadFileAsync(request.ImageFile!, DefaultImageUrl, StorageNames.GameImagesBlob);
                detailsImgUrl = await fileUploader.TryUploadFileAsync(request.DetailsImageFile!, DefaultImageUrl, StorageNames.GameImagesBlob);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }

            Game game = request.MapToGame(imageUrl, detailsImgUrl, userId!);

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Created Game: {gameName}", game.Name);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame,
                new { id = game.Id },
                game.MapToResponse()); 
                //game.MapToResponse(cdnUrlTransformer.TransformToCdnUrl)); // Not using Front Door, too expensive.
        })
        .WithName(EndpointNames.PostGame)
        .WithParameterValidation() // This comes from nuget package MinimalApis.Extensions
        .DisableAntiforgery()
        .RequireAuthorization(Policies.AdminAccess);
    }
}
