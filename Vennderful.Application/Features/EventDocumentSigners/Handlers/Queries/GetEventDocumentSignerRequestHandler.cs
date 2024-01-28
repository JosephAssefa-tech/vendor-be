using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Customers.DTOs;
using MediatR;
using Vennderful.Application.Features.EventDocumentSigners.Requests;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.EventDocumentSigners.Responses;

namespace Vennderful.Application.Features.EventDocumentSigners.Handlers.Queries
{
    public class GetEventDocumentSignerRequestHandler : IRequestHandler<GetEventDocumentSignerRequest, GetEventDocumentSignerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventDocumentSignerRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventDocumentSignerResponse> Handle(GetEventDocumentSignerRequest request, CancellationToken cancellationToken)
        {
            var response = new GetEventDocumentSignerResponse();
            var eventDocumentSigner = await _unitOfWork.eventDocumentSignerRepository.GetEventDocumentSignerByEventDocumentIdAndSignerId(request.EventDocumentId, request.SignerId);

            if (eventDocumentSigner == null)
            {
                response.Success = false;
                response.Message = "Either the event document or the signer not found.";
                return response;
            }

            var sender = await _unitOfWork.UserProfileRepository.GetUserProfileByUserId(eventDocumentSigner.SignatureRequestSender);

            if (sender == null)
            {
                response.Success = false;
                response.Message = "The signature requester not found.";
                return response;
            }

            var document = await _unitOfWork.NewDocumentRepository.GetById(request.DocumentId);

            if (document == null)
            {
                response.Success = false;
                response.Message = "The document not found.";
                return response;
            }

            var signer = await _unitOfWork.UserProfileRepository.GetUserProfileByUserId(request.SignerId);

            if (signer == null)
            {
                response.Success = false;
                response.Message = "The signer not found.";
                return response;
            }

            response.Success = true;
            response.Data = new DTOs.EventDocumentSignerDTO
            {
                SentFrom = sender.LastName + " " + sender.FirstName,
                LastChange = document.LastModified,
                SentDate = eventDocumentSigner.Created,
                SignerName = signer.LastName + " " + signer.FirstName,
            };
            return response;
        }
    }
}
