using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Vennderful.Application.Contracts.BlobStorage.Blob;
using Vennderful.Application.Models.UploadDocuments;
using Vennderful.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Vennderful.Application.Extensions;

namespace BlobStorage.Blob
{
    public class BlobStorageService : IDocumentUploadStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadBlobAsync(Stream stream, string fileName)
        {
            var containerName = "venderfullcontainer";

            // Generate a unique blob name
            var uniqueBlobName = GenerateUniqueBlobName(fileName);

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(uniqueBlobName);

            await blobClient.UploadAsync(stream);
            stream.Close();

            return blobClient.Uri.ToString();
        }

        public async Task<UploadedImageUrlDTO> UploadImageAsync(UploadImagesDto file)
        {
            if (file == null)
            {
                return null;
            }
            var containerClient = _blobServiceClient.GetBlobContainerClient("venderfullcontainer");
            var urls = new List<string>();
            var blobClient = containerClient.GetBlobClient(GetPathWithDocumentName(file.Name, file.CompanyId, file.Category));
            await blobClient.UploadAsync(file.Content, new BlobHttpHeaders { ContentType = file.ContentType });
            string url = blobClient.Uri.ToString();
            return new UploadedImageUrlDTO(url);




        }
        public string GetPathWithDocumentName(string name, string companyId, string Category)
        {
            string shortClientSideFileNameWithoutExt = Path.GetFileNameWithoutExtension(name).TruncateLongString(10);
            string ext = Path.GetExtension(name);
            string basePath = $"{companyId}/{Category}/";
            return basePath + shortClientSideFileNameWithoutExt + ext;
        }

        private string GenerateUniqueBlobName(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var uniqueId = Guid.NewGuid().ToString();
            var originalFileName = Path.GetFileNameWithoutExtension(fileName);
            var counter = 0;
            var uniqueBlobName = $"{originalFileName}_{uniqueId}{fileExtension}";

            // Check if the generated unique blob name already exists
            while (BlobExists(uniqueBlobName))
            {
                counter++;
                uniqueBlobName = $"{originalFileName}_{uniqueId}({counter}){fileExtension}";
            }

            return uniqueBlobName;
        }

        private bool BlobExists(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("venderfullcontainer");
            var blobClient = containerClient.GetBlobClient(blobName);

            return blobClient.Exists();
        }

    }
}
