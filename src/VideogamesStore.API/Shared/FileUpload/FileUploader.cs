namespace VideogamesStore.API.Shared.FileUpload;

public class FileUploader(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
{

    public async Task<string> TryUploadFileAsync(IFormFile file, string defaultImageUrl, string folderPath)
    {
        if(file is not null) 
        {
            var fileUploadResult = await UploadFileAsync(file, folderPath);

            if(!fileUploadResult.IsSuccess)
                throw new InvalidOperationException(fileUploadResult.ErrorMessage);

            return fileUploadResult.FileUrl!;
        }

        return defaultImageUrl;
    }

    private async Task<FileUploadResult> UploadFileAsync(IFormFile file, string folderPath)
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

        // Handle directory and path creation
        string targetFolderName = Path.Combine(environment.WebRootPath, folderPath);

        if (!Directory.Exists(targetFolderName))
            Directory.CreateDirectory(targetFolderName);

        string fileName = Guid.NewGuid().ToString() + fileExtension;
        string fullPath = Path.Combine(targetFolderName, fileName);

        // Save the file
        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        // Return the file URL
        HttpContext? httpContext = httpContextAccessor.HttpContext;
        return new FileUploadResult { 
            IsSuccess = true, 
            FileUrl = $"{httpContext?.Request.Scheme}://{httpContext?.Request.Host}/{folderPath}/{fileName}" 
        };
    }
}
