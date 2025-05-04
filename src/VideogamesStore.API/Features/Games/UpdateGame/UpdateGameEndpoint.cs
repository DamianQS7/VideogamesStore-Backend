using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;
using VideogamesStore.API.Shared.Authorization;
using VideogamesStore.API.Shared.FileUpload;
using static VideogamesStore.API.Features.Games.UpdateGame.UpdateGameDtos;

namespace VideogamesStore.API.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapPutGame(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (
            Guid id, 
            [FromForm] UpdateGameRequest request, 
            GameStoreContext dbContext,
            FileUploader fileUploader,
            ClaimsPrincipal user) => 
        {
            if(user?.Identity?.IsAuthenticated == false)
                 return Results.Unauthorized();

            string? userId = user?.FindFirstValue(JwtRegisteredClaimNames.Email) ?? 
                             user?.FindFirstValue(CustomClaimTypes.UserId);

            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            Game? existingGame = await dbContext.Games.FindAsync(id);
            
            if (existingGame is null)
                return Results.NotFound();

            string imageUrl;
            string detailsImageUrl;

            try
            {
                imageUrl = await fileUploader.TryUploadFileAsync(
                    request.ImageFile!, existingGame.ImageUrl, StorageNames.GameImagesFolder);
                detailsImageUrl = await fileUploader.TryUploadFileAsync(
                    request.DetailsImageFile!, existingGame.DetailsImageUrl, StorageNames.GameImagesFolder);
            }
            catch(InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }

            existingGame.UpdateGame(request, imageUrl, detailsImageUrl, userId);
            
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        })
        .WithName(EndpointNames.UpdateGame)
        .WithParameterValidation()
        .DisableAntiforgery()
        .RequireAuthorization(Policies.AdminAccess);
    }
}
