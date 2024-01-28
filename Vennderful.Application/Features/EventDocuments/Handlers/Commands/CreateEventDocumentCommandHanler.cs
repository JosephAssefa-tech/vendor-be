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
using Vennderful.Application.Features.EventDocuments.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.EventDocuments.Handlers.Commands
{
    public class CreateEventDocumentCommandHanler : IRequestHandler<CreateEventDocumentCommand, CreateEventDocumentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventDocumentCommandHanler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateEventDocumentResponse> Handle(CreateEventDocumentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventDocumentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventDocument);

            var response = new CreateEventDocumentResponse();
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var eventDocument = _mapper.Map<EventDocument>(request.CreateEventDocument);
            eventDocument = await _unitOfWork.eventDocumentRepository.AddAsync(eventDocument);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateEventDocumentsDto>(eventDocument);


            return response;

        }
    }
}
