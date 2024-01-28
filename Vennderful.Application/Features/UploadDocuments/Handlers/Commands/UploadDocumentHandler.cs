using System.Linq;
using AutoMapper;
using MediatR;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.BlobStorage.Blob;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.UploadDocuments.Requests;
using Vennderful.Application.Features.UploadDocuments.Responses;
using Vennderful.Domain.Entities;
using System.Collections.Generic;

namespace Vennderful.Application.Features.UploadDocuments.Handlers.Commands
{
    public class UploadDocumentHandler : IRequestHandler<DocumentRequest, DocumentResponse>
    {
        private readonly IDocumentUploadStorageService _blobStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UploadDocumentHandler(IDocumentUploadStorageService blobStorageService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _blobStorageService = blobStorageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DocumentResponse> Handle(DocumentRequest request, CancellationToken cancellationToken)
        {
            // Validate file format
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".txt" };
            var fileExtension = Path.GetExtension(request.DocumentFile.FileName);
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid file format. Only PDF, DOC, DOCX, and TXT files are allowed.");
            }

            // Check file size
            if (request.DocumentFile.Length > 2 * 1024 * 1024)
            {
                throw new ArgumentException("File size exceeds the maximum limit of 2MB.");
            }

            // Get the original file name and unique file name
            var (originalFileName, uniqueFileName) = await GetFileNameFromContentDisposition(request.DocumentFile.ContentDisposition);

            // Upload file to Azure Blob Storage with the generated unique file name
            var blobUrl = await _blobStorageService.UploadBlobAsync(request.DocumentFile.OpenReadStream(), uniqueFileName);

            // Save document information to the local database
            var documentDto = new Document
            {
                DocumentUrl = blobUrl,
                DocumentCategory = request.Category,
                DocumentName = uniqueFileName,
                CompanyId = request.CompanyId,
            };
            await _unitOfWork.NewDocumentRepository.AddAsync(documentDto);
            await _unitOfWork.Save();

            return new DocumentResponse
            {
                DocumentUrl = blobUrl,
                Category = request.Category,
                DocumentName = originalFileName
            };
        }

        private async Task<string> GenerateUniqueFileName(string fileName, IEnumerable<Document> existingFiles)
        {
            var fileExtension = Path.GetExtension(fileName);
            var uniqueId = Guid.NewGuid().ToString();
            var originalFileName = Path.GetFileNameWithoutExtension(fileName);
            var counter = 0;
            var uniqueFileName = originalFileName + fileExtension;

            // Check if the generated unique file name already exists
            while (await FileExists(uniqueFileName, existingFiles))
            {
                counter++;
                uniqueFileName = $"{originalFileName}({counter}){fileExtension}";
            }

            return uniqueFileName;
        }






        private async Task<bool> FileExists(string fileName, IEnumerable<Document> existingFiles)
        {
            return await Task.Run(() => existingFiles.Any(f => f.DocumentName == fileName));
        }




        private async Task<(string OriginalFileName, string UniqueFileName)> GetFileNameFromContentDisposition(string contentDisposition)
        {
            if (!ContentDispositionHeaderValue.TryParse(contentDisposition, out var contentDispositionHeader))
            {
                // Invalid content disposition header format
                throw new ArgumentException("Invalid content disposition header", nameof(contentDisposition));
            }

            var fileName = contentDispositionHeader.FileName.ToString();

            // Remove surrounding quotes if present
            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
            {
                fileName = fileName.Trim('"');
            }

            // Check if the file name already exists in the database
            var existingFiles = await _unitOfWork.NewDocumentRepository.GetAllAsync();

            // Generate unique file name
            var uniqueFileName = await GenerateUniqueFileName(fileName, existingFiles);

            return (fileName, uniqueFileName);
        }



    }
}