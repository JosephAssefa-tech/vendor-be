using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.Events.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.Events.Handlers.Commands
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, UpdateEventResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateEventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEventDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateEventDto);

            var response = new UpdateEventResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            // Fetch the existing event by eventId
            var existingEvent = await _unitOfWork.eventRepository.GetById(request.UpdateEventDto.Id);

            if (existingEvent == null)
            {
                response.Success = false;
                response.Message = "Updation Failed.";
                response.Errors = new List<string> { "Event Not Found." };
                return response;
            }

            // Update the properties of the existing event
            existingEvent.EventName = request.UpdateEventDto.EventName;
            existingEvent.TypeOfEvents = request.UpdateEventDto.TypeOfEvents;
            existingEvent.NumberOfGuests = request.UpdateEventDto.NumberOfGuests;
            existingEvent.DressCodes = request.UpdateEventDto.DressCodes;
            existingEvent.CoverPhoto = request.UpdateEventDto.CoverPhoto;
            existingEvent.EventSetupTime = request.UpdateEventDto.EventSetupTime;
            existingEvent.EventStartDateAndTime = request.UpdateEventDto.EventStartDateAndTime;
            existingEvent.EventEndDateAndTime = request.UpdateEventDto.EventEndDateAndTime;

            // Save the updated event
            await _unitOfWork.eventRepository.UpdateAsync(existingEvent);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Updated Successfully.";
            response.Data = _mapper.Map<UpdateEventDto>(existingEvent);

            return response;
        }
    }
}
