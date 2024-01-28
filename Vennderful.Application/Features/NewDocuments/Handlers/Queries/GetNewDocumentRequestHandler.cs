using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Queries
{
    public class GetNewDocumentRequestHandler : IRequestHandler<GetNewDocumentsRequest, GetNewDocumentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetNewDocumentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetNewDocumentResponse> Handle(GetNewDocumentsRequest request, CancellationToken cancellationToken)
        {
            var newDocuments = (await _unitOfWork.NewDocumentRepository.GetAllAddedDocuments(request.CompanyId)).OrderByDescending(c => c.Created);
            var response = new GetNewDocumentResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<ListNewDocumentDto>>(newDocuments);
            return response;
        }
    }
}
