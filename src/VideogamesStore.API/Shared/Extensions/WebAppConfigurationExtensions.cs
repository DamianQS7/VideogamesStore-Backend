using Azure.Core;
using Azure.Identity;

namespace VideogamesStore.API.Shared.Extensions;

public static class WebAppConfigurationExtensions
{
    public static TokenCredential GetAzureCredential(this IConfiguration configuration)
    {
        return new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ManagedIdentityClientId = configuration["AZURE_MANAGED_ID_CLIENT_ID"]
        });
    }
}
