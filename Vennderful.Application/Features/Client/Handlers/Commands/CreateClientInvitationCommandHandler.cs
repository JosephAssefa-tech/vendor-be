using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Domain.Entities;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;

namespace Vennderful.Application.Features.Client.Handlers.Commands
{
    public class CreateClientInvitationCommandHandler : IRequestHandler<CreateClientInvitationCommand, CreateClientInvitationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateClientInvitationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateClientInvitationResponse> Handle(CreateClientInvitationCommand request,
            CancellationToken cancellationToken)
        {
            var response = new CreateClientInvitationResponse();

            try
            {
                var notificationResponse = new List<Notification>();

                // loop into existing clients
                foreach (var c in request.CreateClientInvitationDTO.clientId)
                {
                    var objClient = await _unitOfWork.clientRepository.GetByIdAsync(c);
                    if (objClient != null)
                    {
                        // check if client is already invited to event
                        var objExistingEvent = await _unitOfWork.eventClientRepository.GetByEventAndClientId(c, request.EventId);

                        if (objExistingEvent == null)
                        {
                            var eventClient = new EventClient()
                            {
                                EventId = request.EventId,
                                ClientId = c,
                                Note = "Existing Client Invitation",
                                Status = Domain.Enums.InvitationStatus.Pending,
                            };

                            eventClient = await _unitOfWork.eventClientRepository.AddAsync(eventClient);
                            await _unitOfWork.Save();

                            // get event name
                            var objEvent = await _unitOfWork.eventRepository.GetById(request.EventId);

                            // add to notification table
                            var notification = new Notification()
                            {
                                UserId = c, //TODO: get from userprofile later when userProfileId column is added to Clients table
                                NotificationType = Domain.Enums.NotificationType.Invitation,
                                NotificationMethod = Domain.Enums.NotificationMethod.System,
                                Content = $"You have been invited to the event {objEvent.EventName}.",
                                ClientId = c,
                                EventId = request.EventId,
                                HasBeenRead = false
                            };
                            notification = await _unitOfWork.notificationRepository.AddAsync(notification);
                            await _unitOfWork.Save();
                            notificationResponse.Add(notification);
                        }
                        else
                        {
                            // notification already exists for the event, return an error response
                            response.Success = false;
                            response.Message = $"Client {objClient.FirstName} {objClient.LastName} has already been invited to this event";
                            response.Errors = new List<string>() { response.Message };
                            return response;
                        }
                    }
                    else
                    {
                        // client not found, return an error response
                        response.Success = false;
                        response.Message = $"Client {c} not found";
                        response.Errors = new List<string>() { response.Message };
                        return response;
                    }
                }
                response.Success = true;
                response.Message = "Clients Invited Successfully!";
                response.Data = _mapper.Map<List<ClientInvitationDTO>>(notificationResponse);

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong";
                response.Errors = new List<string>() { ex.Message };
                return response;
            }
        }
    }
}
