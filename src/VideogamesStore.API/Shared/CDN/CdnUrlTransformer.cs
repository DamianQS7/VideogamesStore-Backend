using System;
using Azure.Storage.Blobs;

namespace VideogamesStore.API.Shared.CDN;

public class CdnUrlTransformer(IConfiguration configuration, BlobServiceClient blobServiceClient)
{
    public string TransformToCdnUrl(string storageUrl)
    {
        ArgumentNullException.ThrowIfNull(storageUrl);

        string? frontDoorHostName = configuration["AZURE_FRONTDOOR_HOSTNAME"];

        if (string.IsNullOrEmpty(frontDoorHostName)) 
            return storageUrl;

        if (Uri.TryCreate(storageUrl, UriKind.Absolute, out var uri))
        {
            string? storageHost = blobServiceClient.Uri.Host;

            if (!string.Equals(uri.Host, storageHost, StringComparison.OrdinalIgnoreCase))
                return storageUrl;

            UriBuilder uriBuilder = new(uri) { Host = frontDoorHostName };

            return uriBuilder.Uri.ToString();
        }

        return storageUrl;
    }
}
