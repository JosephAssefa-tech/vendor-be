using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventAndRooms.Dto;
using Vennderful.Application.Features.EventAndRooms.Requests;
using Vennderful.Application.Features.EventAndRooms.Responses;
using Vennderful.Application.Features.EventAndRooms.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.EventAndRooms.Handlers.Commands
{
    public class CreateEventAndRoomCommandHandler : IRequestHandler<CreateEventAndRoomsCommand, CreateEventAndRoomResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventAndRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateEventAndRoomResponse> Handle(CreateEventAndRoomsCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventAndRoomDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventAndRoomDto);

            var response = new CreateEventAndRoomResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var eventId = request.CreateEventAndRoomDto.EventId;
            var roomIds = request.CreateEventAndRoomDto.RoomId;

            // Create EventAndClient entities and associate them with the event
            var eventAndRooms = new EventAndRoom
            {
                EventId = eventId,
                CompanyId = request.CreateEventAndRoomDto.CompanyId,
                RoomId = roomIds
            };
            eventAndRooms = await _unitOfWork.EventAndRoomRepository.AddAsync(eventAndRooms);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateEventAndRoomDto>(eventAndRooms);

            return response;
        }
    }
}
