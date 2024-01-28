using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Responses;
using Vennderful.Application.Features.VenueProfile.Validators;
using Vennderful.Domain.Entities;
using Vennderful.Application.Features.Events.Validators;
using System.Linq;
using AutoMapper;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Handlers.Commands
{
    public class EditEventHandler : IRequestHandler<EditEventCommand, EditEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditEventHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EditEventResponse> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditEventDTOValidator();
            var validationResult = await validator.ValidateAsync(request.Data);

            var response = new EditEventResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingEvent = await _unitOfWork.eventRepository.GetById(request.Data.Id);

            var updatedEvent = _mapper.Map<Event>(request.Data);

            if (existingEvent != null)
            {
                existingEvent.EventName = updatedEvent.EventName;
                existingEvent.TypeOfEvents = updatedEvent.TypeOfEvents;
                existingEvent.DressCodes = updatedEvent.DressCodes;
                existingEvent.NumberOfGuests = updatedEvent.NumberOfGuests;
                existingEvent.CoverPhoto = updatedEvent.CoverPhoto;
                existingEvent.EventStartDateAndTime = updatedEvent.EventStartDateAndTime;
                existingEvent.EventEndDateAndTime = updatedEvent.EventEndDateAndTime;
                existingEvent.EventSetupTime = updatedEvent.EventSetupTime;

                await _unitOfWork.eventRepository.UpdateAsync(existingEvent);
               
                    if (request.Data.EventRooms != null && request.Data.EventRooms.Any())
                    {
                        var existingRoomsByEvent = await _unitOfWork.EventAndRoomRepository.GetEventAndRoomsByEventId(existingEvent.Id);
                        var existingRoooms = existingRoomsByEvent.Select(x => x.RoomId.FirstOrDefault().ToString()).ToList();
                        if (!request.Data.EventRooms.SequenceEqual(existingRoooms))
                        {
                            var toBeAdded = request.Data.EventRooms.Except(existingRoooms).ToList();
                            var toBeDeleted = existingRoooms.AsEnumerable().Except(request.Data.EventRooms).ToList();

                            if (toBeAdded != null && toBeAdded.Any())
                            {
                                foreach (var room in toBeAdded)
                                {

                                    if (!existingRoooms.Contains(room))
                                    {
                                        await _unitOfWork.EventAndRoomRepository.AddAsync(new EventAndRoom()
                                        {
                                            Id = Guid.NewGuid(),
                                            CompanyId = existingEvent.CompanyId,
                                            EventId = existingEvent.Id,
                                            RoomId = new List<Guid>()
                                {
                                    new Guid(room.ToString())
                                }
                                        });
                                    }
                                }
                            }
                            if (toBeDeleted != null && toBeDeleted.Any())
                            {
                                foreach (var room in toBeDeleted)
                                {
                                      var item = existingRoomsByEvent.Where(x => x.EventId == existingEvent.Id && x.RoomId.FirstOrDefault() == Guid.Parse(room)).FirstOrDefault();
                                    await _unitOfWork.EventAndRoomRepository.DeleteAsync(item);
                                }
                            }
                        }
                    }
                
            }
                await _unitOfWork.Save();
            
            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = _mapper.Map<EditEventDTO>(existingEvent);

            return response;
        }



    }
}
