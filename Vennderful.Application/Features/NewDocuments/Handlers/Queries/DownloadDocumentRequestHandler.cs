using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using MediatR;
using Vennderful.Application.Contracts.Interfaces;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Queries
{
    public class DownloadDocumentRequestHandler : IRequestHandler<DownloadDocumentRequest, DownloadDocumentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public DownloadDocumentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<DownloadDocumentResponse> Handle(DownloadDocumentRequest request, CancellationToken cancellationToken)
        {
            var document = await _unitOfWork.NewDocumentRepository.GetById(request.Id);

            var response = new DownloadDocumentResponse();
            if (document == null)
            {
                response.Success = false;
                response.Message = "Document Not Found";
                return response;
            }

            response.Success = true;
            response.DocumentName = document.DocumentName;
            response.DocumentBody = document.DocumentBody;
            
            // Azure Blob
            if (!string.IsNullOrEmpty(document.DocumentUrl))
            {
                response.DocumentUrl = document.DocumentUrl;
                response.FileType = _fileService.GetMimeTypeForFileExtension(document.DocumentUrl);
            }

            return response;
        }
    }
}
