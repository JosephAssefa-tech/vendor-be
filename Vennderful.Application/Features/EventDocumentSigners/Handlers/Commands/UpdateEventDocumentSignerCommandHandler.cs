using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Vennderful.Application.Features.EventDocumentSigners.Requests;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.EventDocumentSigners.Responses;
using Vennderful.Application.Features.Customers.Responses;

namespace Vennderful.Application.Features.EventDocumentSigners.Handlers.Commands
{
    public class UpdateEventDocumentSignerCommandHandler : IRequestHandler<UpdateEventDocumentSignerCommand, UpdateEventDocumentSignerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventDocumentSignerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateEventDocumentSignerResponse> Handle(UpdateEventDocumentSignerCommand request,
            CancellationToken cancellationToken)
        {
            var eventDocumentSigner = await _unitOfWork.eventDocumentSignerRepository.GetEventDocumentSignerByEventDocumentIdAndSignerId(request.EventDocumentId, request.SignerId);

            var response = new UpdateEventDocumentSignerResponse();
            if (eventDocumentSigner == null)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Either the document or the signer Not Found." };
                return response;
            }

            eventDocumentSigner.DocumentStatus = Domain.Enums.DocumentStatus.Completed;
            await _unitOfWork.eventDocumentSignerRepository.UpdateAsync(eventDocumentSigner);

            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Bad Request." };

                return response;
            }

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = "signed";

            return response;
        }
    }
}
