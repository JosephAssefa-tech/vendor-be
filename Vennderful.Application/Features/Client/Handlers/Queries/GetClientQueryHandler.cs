using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using MediatR;
using System.Linq;
using System.Runtime.CompilerServices;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Client.Handlers.Queries
{
    public class GetClientQueryHandler : IRequestHandler<GetClientRequest, GetClientResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetClientResponse> Handle(GetClientRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetClientResponse();
            try
            {
                var client = (await _unitOfWork.clientRepository.GetClientById(request.ClientId));

                var clientDto = new ClientDTO
                {
                    Id = client.Id,
                    Created = client.Created,
                    CreatedBy = client.CreatedBy,
                    LastModified = client.LastModified,
                    LastModifiedBy = client.LastModifiedBy,
                    IsActive = client.IsActive,
                    Email = client.Email,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Gender = client.Gender,
                    AccountType = client.AccountType,
                    CompanyName = client.CompanyName,
                    Phone = client.Phone,
                    Address = client.Address,
                    Events = client.EventClients?.Select(ec => new EventDTO
                    {
                        EventName = ec.Event.EventName,
                        EventID = ec.Event.EventID,
                        EventType = ec.Event.TypeOfEvents.ToString(),
                        DressCode = ec.Event.DressCodes.ToString(),
                        CoverPhoto = ec.Event.CoverPhoto,
                        EventStartDate = ec.Event.EventStartDateAndTime.ToShortDateString(),
                        EventStartTime = ec.Event.EventStartDateAndTime.ToShortTimeString(),
                        EventEndDate = ec.Event.EventEndDateAndTime.ToShortDateString(),
                        EventEndTime = ec.Event.EventEndDateAndTime.ToShortTimeString(),
                        EventSetupTime = ec.Event.EventSetupTime,
                    //    EventRoom = ec.Event.Room,
                        CompanyId = ec.Event.CompanyId,
                        NumberOfGuests = ec.Event.NumberOfGuests,
                   //     EventLocation = ec.Event.EventAndRooms,
                        Status = ec.Event.Status
                    }).ToList()
                };

                response.Success = true;
                response.Data = clientDto;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new ClientDTO();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
