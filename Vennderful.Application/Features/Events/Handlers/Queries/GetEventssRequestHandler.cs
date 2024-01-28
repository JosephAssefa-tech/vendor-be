using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.Events.Handlers.Queries
{
    public class GetEventsRequestHandler : IRequestHandler<GetEventsRequest, GetEventsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //public async Task<GetEventsResponse> Handle(GetEventsRequest request,
        //    CancellationToken cancellationToken)
        //{
        //    var events = (await _unitOfWork.eventRepository.GetAllAsync()).OrderBy(c => c.Created);
        //    var response = new GetEventsResponse();
        //    response.Success = true;
        //    response.Data = _mapper.Map<List<ListEventDTO>>(events);
        //    return response;
        //}
        public async Task<GetEventsResponse> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.eventRepository.GetAllWithClientsVenueRoomsAsync(request.CompanyId);
            var venueAccountInformation = await _unitOfWork.VenueAccountInformationRepository.GetById(request.CompanyId);

            var response = new GetEventsResponse
            {
                Success = true,
                Data = events.Select(e => new ListEventDTO
                {
                    Id = e.Id,
                    EventName = e.EventName,
                    EventStartDateAndTime = e.EventStartDateAndTime,
                    EventEndDateAndTime = e.EventEndDateAndTime,
                    EventID = e.EventID,
                    CoverPhoto = e.CoverPhoto,
                    Client = e.EventClients.Select(ec => new ListClientDTO
                    {
                        Id = ec.Client.Id,
                        FirstName = ec.Client.FirstName,
                        LastName = ec.Client.LastName,
                    }).ToList(),
                    VenueAccountInformation = new VenueAccountInformationDto
                    {
                        Id = venueAccountInformation.Id,
                        CompanyName = venueAccountInformation.CompanyName,
                        Address = new Domain.ValueObjects.Address
                        {
                            Street = venueAccountInformation.Address.Street,
                            City = venueAccountInformation.Address.City,
                            State = venueAccountInformation.Address.State,
                            Country = venueAccountInformation.Address.Country,
                            ZipCode = venueAccountInformation.Address.ZipCode
                        },
                        // ... (other properties)
                    }
                }).ToList()
            };

            return response;
        }

    }
}
