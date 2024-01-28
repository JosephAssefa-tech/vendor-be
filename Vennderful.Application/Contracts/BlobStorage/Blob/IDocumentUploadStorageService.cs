using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Models.UploadDocuments;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Contracts.BlobStorage.Blob
{
    public interface IDocumentUploadStorageService
    {
        Task<UploadedImageUrlDTO> UploadImageAsync(UploadImagesDto files);
        Task<string> UploadBlobAsync(Stream stream, string fileExtension);
    }
}
