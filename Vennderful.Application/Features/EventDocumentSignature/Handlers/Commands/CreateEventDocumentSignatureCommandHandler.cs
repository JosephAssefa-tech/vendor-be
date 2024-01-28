using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventDocumentSignature.Dto;
using Vennderful.Application.Features.EventDocumentSignature.Requests;
using Vennderful.Application.Features.EventDocumentSignature.Responses;
using Vennderful.Application.Features.EventDocumentSignature.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.EventDocumentSignature.Handlers.Commands
{
    public class CreateEventDocumentSignatureCommandHandler : IRequestHandler<CreateEventDocumentSignerCommand, CreateEventDocumentSignatureResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventDocumentSignatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateEventDocumentSignatureResponse> Handle(CreateEventDocumentSignerCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventDocumentSignatureDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventDocumentSignatureDto);

            var response = new CreateEventDocumentSignatureResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var eventDocumentSignatureList = new List<EventDocumentSigner>();

            foreach (var signerId in request.CreateEventDocumentSignatureDto.SignerId)
            {
                var eventDocumentSignature = new EventDocumentSigner
                {
                    EventDocumentId = request.CreateEventDocumentSignatureDto.EventDocumentId,
                    SignerId = signerId,
                    SignatureRequestSender = request.CreateEventDocumentSignatureDto.SignatureRequestSender
                };

                eventDocumentSignatureList.Add(eventDocumentSignature);
            }

            foreach (var eventDocumentSignature in eventDocumentSignatureList)
            {
                // Save the document for each signer in your table
                await _unitOfWork.eventDocumentSignerRepository.AddAsync(eventDocumentSignature);
            }

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = new CreateEventDocumentSignatureDTO
            {
                EventDocumentId = eventDocumentSignatureList[0].EventDocumentId, 
                SignerId = eventDocumentSignatureList.Select(es => es.SignerId).ToList(),
                SignatureRequestSender = eventDocumentSignatureList[0].SignatureRequestSender ,
                Id = eventDocumentSignatureList[0].Id
            };


            return response;
        }




    }
}
