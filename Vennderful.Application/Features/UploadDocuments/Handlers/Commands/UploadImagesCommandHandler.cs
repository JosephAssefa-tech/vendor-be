using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.BlobStorage.Blob;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.UploadDocuments.Requests;
using Vennderful.Application.Features.UploadDocuments.Responses;
using Vennderful.Application.Models.UploadDocuments;

namespace Vennderful.Application.Features.UploadDocuments.Handlers.Commands
{
    public class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand, UploadedImageUrlDTO>

    {
        private readonly IDocumentUploadStorageService _filseStorageService;

        public UploadImagesCommandHandler(IDocumentUploadStorageService filseStorageService)
        {
            _filseStorageService = filseStorageService;      
        }

        public async Task<UploadedImageUrlDTO> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
        {
            var url = await _filseStorageService.UploadImageAsync(request.File);
            return url;
        }
    }
}
