using Microsoft.AspNetCore.Mvc;
using VideogamesStore.API.Data;
using VideogamesStore.API.Features.Games.Constants;
using VideogamesStore.API.Models;
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
            FileUploader fileUploader) => 
        {
            Game? existingGame = await dbContext.Games.FindAsync(id);
            
            if (existingGame is null)
                return Results.NotFound();

            existingGame.UpdateWithRequest(request);

            if (request.ImageFile is not null)
            {
                var fileUploadResult = await fileUploader.UploadFileAsync(
                    request.ImageFile, StorageNames.GameImagesFolder
                );
                
                if (!fileUploadResult.IsSuccess)
                    return Results.BadRequest(fileUploadResult.ErrorMessage);

                existingGame.UpdateImageUrl(fileUploadResult.FileUrl!);
            }

            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        })
        .WithName(EndpointNames.UpdateGame)
        .WithParameterValidation()
        .DisableAntiforgery();
    }
}
