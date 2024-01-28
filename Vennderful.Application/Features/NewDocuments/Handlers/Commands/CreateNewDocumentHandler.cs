using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Features.NewDocuments.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.NewDocuments.Handlers.Commands
{
    public class CreateNewDocumentHandler : IRequestHandler<CreateNewDocumentCommand, CountNewlAddedDocumentsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateNewDocumentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CountNewlAddedDocumentsResponse> Handle(CreateNewDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateNewDocumentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateNewDocumentDto);

            var response = new CountNewlAddedDocumentsResponse();
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var document = _mapper.Map<Document>(request.CreateNewDocumentDto);
            document = await _unitOfWork.NewDocumentRepository.AddAsync(document);

            await _unitOfWork.Save();

            var result = await _unitOfWork.NewDocumentRepository.GetAllAsync();

            var addEventtDocument = new EventDocument()
            {
                DocumentId = document.Id,
                EventId = request.CreateNewDocumentDto.EventId,
                DocumentSignerType = (Domain.Enums.DocumentSignerType)1,
            };
            await _unitOfWork.eventDocumentRepository.AddAsync(addEventtDocument);
            await _unitOfWork.Save();



            CountNewlAddedDocumentsResponse countNewlyAdded = new CountNewlAddedDocumentsResponse();
            countNewlyAdded.CreateNewDocumentDto = _mapper.Map<CreateNewDocumentDto>(document);
            countNewlyAdded.CountDocuments = result.Count();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = countNewlyAdded;

            return response;

        }
    }
}
