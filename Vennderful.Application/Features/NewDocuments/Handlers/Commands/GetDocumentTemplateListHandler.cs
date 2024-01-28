using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Features.NewDocuments.DTOs;
using AutoMapper;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Commands
{
  
    public class GetDocumentTemplateListHandler : IRequestHandler<GetDocumentTemplateListQuery, GetDocumentTemplateListResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDocumentTemplateListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDocumentTemplateListResponse> Handle(GetDocumentTemplateListQuery request, CancellationToken cancellationToken)
        {
            var documentTemplates = await _unitOfWork.NewDocumentRepository.GetAllDocumentTemplates(request.CompanyId);
            if (request.TemplayeName != null)
            {
                documentTemplates = documentTemplates.Where(t => t.DocumentName.ToLower().Equals(request.TemplayeName.ToLower()));
            }
            var response = new GetDocumentTemplateListResponse();
            response.Success = true;
            response.Data = _mapper.Map<List<NewDocumentDto>>(documentTemplates);
            return response;
        }


    }
}
