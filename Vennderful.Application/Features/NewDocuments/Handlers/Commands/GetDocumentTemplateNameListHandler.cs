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
    public class GetDocumentTemplateNameListHandler : IRequestHandler<GetDocumentTemplateNameListQuery, GetDocumentTemplateNameListResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDocumentTemplateNameListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDocumentTemplateNameListResponse> Handle(GetDocumentTemplateNameListQuery request, CancellationToken cancellationToken)
        {
            var documentTemplates = await _unitOfWork.NewDocumentRepository.GetAllDocumentTemplates(request.CompanyId);
             List<string> templates = null;
            if (request.TemplayeName != null)
            {
                documentTemplates = documentTemplates.Where(t => t.DocumentName.ToLower().Contains(request.TemplayeName.ToLower()));
                templates = documentTemplates.Select(n=> n.DocumentName).ToList();
            }
            var response = new GetDocumentTemplateNameListResponse();
                response.Success = true;
            // response.Data = _mapper.Map<List<NewDocumentDto.>>(documentTemplates);
               response.Data = templates;
                return response;
        }


    }
}
