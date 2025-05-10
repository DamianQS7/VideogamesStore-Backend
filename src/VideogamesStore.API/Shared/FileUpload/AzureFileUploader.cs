using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace VideogamesStore.API.Shared.FileUpload;

public class AzureFileUploader(BlobServiceClient blobServiceClient)
{
    public async Task<string> TryUploadFileAsync(IFormFile file, string defaultImageUrl, string containerName)
    {
        if(file is not null) 
        {
            var fileUploadResult = await UploadFileAsync(file, containerName);

            if(!fileUploadResult.IsSuccess)
                throw new InvalidOperationException(fileUploadResult.ErrorMessage);

            return fileUploadResult.FileUrl!;
        }

        return defaultImageUrl;
    }

    public async Task<FileUploadResult> UploadFileAsync(IFormFile file, string containerName)
    {
        // Check if we have a valid file
        if (file == null || file.Length == 0)
            return new FileUploadResult { IsSuccess = false, ErrorMessage = "File is empty." };

        if (file.Length > 10485760)
            return new FileUploadResult { IsSuccess = false, ErrorMessage = "File is too large." };

        string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
        string fileExtension = Path.GetExtension(file.FileName);

        if (!allowedExtensions.Contains(fileExtension) || string.IsNullOrEmpty(fileExtension))
            return new FileUploadResult { IsSuccess = false, ErrorMessage = "Invalid file type." };

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        string fileName = Guid.NewGuid().ToString() + fileExtension;

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();

        using var fileStream = file.OpenReadStream();
        await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType});

        return new FileUploadResult { 
            IsSuccess = true, 
            FileUrl = blobClient.Uri.ToString() 
        };
    }
}
