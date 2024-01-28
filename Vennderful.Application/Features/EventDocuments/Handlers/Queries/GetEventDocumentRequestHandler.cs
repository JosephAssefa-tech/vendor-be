using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventDocuments.Dto;
using Vennderful.Application.Features.EventDocuments.Requests;
using Vennderful.Application.Features.EventDocuments.Responses;

namespace Vennderful.Application.Features.EventDocuments.Handlers.Queries
{
    public class GetEventDocumentRequestHandler : IRequestHandler<GetEventDocumentsRequest, GetEventDocumentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventDocumentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //public async Task<GetEventDocumentResponse> Handle(GetEventDocumentsRequest request, CancellationToken cancellationToken)
        //{
        //    var eventDocument = await _unitOfWork.eventDocumentRepository.GetAllEventAddedDocuments(request.EventId);
        //   // var eventDocumentStatus = await _unitOfWork.eventDocumentSignerRepository.GetAllEventAddedDocumentsStatus(eventDocument.Id);

        //    var response = new GetEventDocumentResponse()
        //    {
        //        Success = true,
        //        Data = eventDocument.Select(e => new ListEventDocumentDto
        //        {
        //            DocumentName = e.Document.DocumentName,
        //           //documentStatus = e.Document.d
        //        }).ToList(),
        //    };

        //    return response;
        //}
      //  my assumeption i don't think to filter the status by eventDocumentId, because it will be unique and not return the all associated documents
        public async Task<GetEventDocumentResponse> Handle(GetEventDocumentsRequest request, CancellationToken cancellationToken)
        {
            var eventDocumentList = await _unitOfWork.eventDocumentRepository.GetAllEventAddedDocuments(request.EventId);
            var response = new GetEventDocumentResponse()
            {
                Success = true,
                Data = new List<ListEventDocumentDto>()
            };

            foreach (var eventDocument in eventDocumentList)
            {
                var eventDocumentStatusList = await _unitOfWork.eventDocumentSignerRepository.GetAllEventAddedDocumentsStatus(eventDocument.Id);

                foreach (var eventDocumentStatus in eventDocumentStatusList)
                {
                    response.Data.Add(new ListEventDocumentDto
                    {
                        DocumentName = eventDocument.Document.DocumentName,
                        documentStatus = eventDocumentStatus.DocumentStatus,
                        Id = eventDocumentStatus.EventDocumentId

                    });
                }
            }

            return response;
        }


    }
}
