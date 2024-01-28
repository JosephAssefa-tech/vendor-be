using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Queries
{
    public class GetNewDownloadedDocumentRequestHandler : IRequestHandler<GetNewDocumentRequest, GetDownloadedDocumentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNewDownloadedDocumentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetDownloadedDocumentResponse> Handle(GetNewDocumentRequest request, CancellationToken cancellationToken)
        {
            var document = await _unitOfWork.NewDocumentRepository.GetById(request.Id, request.CompanyId);
            var response = new GetDownloadedDocumentResponse();
            if (document == null)
            {
                response.Success = false;
                response.Message = "Customer Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<NewDocumentDto>(document);
            return response;
        }
    }
}
