using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.Events.Validators;
using Vennderful.Application.Models.Mail;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.Events.Handlers.Commands
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreateEventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        static Random random = new Random();

        public CreateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<CreateEventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventDTO);

            var response = new CreateEventResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            //var existingEvent = await _unitOfWork.eventRepository.GetEventsByName(request.CreateEventDTO.EventName);
            //if (existingEvent != null)
            //{
            //    response.Success = false;
            //    response.Message = "Event with a similar name already exists.";
            //    response.Errors = new List<string>() { "Event with a similar name already exists." };

            //    return response;
            //}

            var anEvent = _mapper.Map<Domain.Entities.Event>(request.CreateEventDTO);
            anEvent.EventID = random.Next(0, 999999).ToString("D6");
            anEvent.Id = new Guid();
            anEvent.Status = EventStatus.Booked.ToString();
            anEvent.CoverPhoto = "default_cover_photo.jpg";

            // Update specific properties from the request
            anEvent.EventName = "tempEvent";
            anEvent.TypeOfEvents = TypeOfEvent.Social;
            anEvent.DressCodes = DressCode.BusinessCasual;
            anEvent.NumberOfGuests = 0;
            anEvent.EventSetupTime = "00:00";
            anEvent.EventStartDateAndTime = DateTime.MinValue;
            anEvent.EventEndDateAndTime = DateTime.MinValue;

            anEvent = await _unitOfWork.eventRepository.AddAsync(anEvent);
            await _unitOfWork.Save();
            //check the below code if to test if the event Id already exists
          //  anEvent = await _unitOfWork.eventRepository.GetEventsByName(request.CreateEventDTO.EventName);

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateEventDTO>(anEvent);

            return response;
        }

    }
}
