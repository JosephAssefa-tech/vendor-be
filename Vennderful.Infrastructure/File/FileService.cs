using Microsoft.AspNetCore.StaticFiles;
using Vennderful.Application.Contracts.Interfaces;

namespace Vennderful.Infrastructure.File
{
    public class FileService : IFileService
    {
        public string GetMimeTypeForFileExtension(string filePath)
        {
            const string DefaultContentType = "application/octet-stream";

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = DefaultContentType;
            }

            return contentType;
        }
    }
}
