using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.Events.Handlers.Queries
{
    public class GetEventRequestHandler : IRequestHandler<GetEventByIdRequest, GetEventByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventByIdResponse> Handle(GetEventByIdRequest request, CancellationToken cancellationToken)
        {
            var anEvent = await _unitOfWork.eventRepository.GetById(request.Id, request.CompanyId);
            var response = new GetEventByIdResponse();
            if (anEvent == null)
            {
                response.Success = false;
                response.Message = "Event Not Found.";
                return response;
            }
            var venue = await _unitOfWork.VenueAccountInformationRepository.GetById(request.CompanyId);
            var eventDTO = _mapper.Map<EditEventDTO>(anEvent);
            if (venue != null && venue.Address != null)
            {
                eventDTO.EventLocation = $"{venue.Address.Street}, {venue.Address.City}, {venue.Address.State} {venue.Address.ZipCode}";
                eventDTO.Venue = venue.CompanyName;
            }
            eventDTO.TypeOfEvents = eventDTO.TypeOfEvents.ToString();
            eventDTO.DressCodes = eventDTO.DressCodes.ToString();
            var eventRooms = await _unitOfWork.EventAndRoomRepository.GetEventAndRoomsByEventId(request.Id);
            if (eventRooms != null)
            {

                eventDTO.EventRooms = eventRooms.Select(x => x.RoomId[0].ToString()).ToList<string>();
            }
            response.Success = true;
            response.Data = eventDTO;
            return response;
        }
    }
}
