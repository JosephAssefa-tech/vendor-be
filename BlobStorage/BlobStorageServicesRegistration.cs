using Azure.Storage.Blobs;
using BlobStorage.Blob;
using BlobStorage.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vennderful.Application.Contracts.BlobStorage.Blob;

namespace BlobStorage
{
    public static class BlobStorageServicesRegistration
    {
        public static IServiceCollection AddBlobStorageServices(
    this IServiceCollection services,
    IConfiguration configuration)
        {
            var blobStorageSettings = new BlobStorageSettings();
            configuration.GetSection(BlobStorageSettings.SettingName).Bind(blobStorageSettings);
            services.AddSingleton(x => new BlobServiceClient(blobStorageSettings.ConnectionString));
            services.AddScoped<IDocumentUploadStorageService, BlobStorageService>();

            return services;


        }
    }
}
