using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.EventRoom.Requests;
using Vennderful.Application.Features.EventRoom.Responses;
using Vennderful.Application.Features.EventRoom.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.EventRoom.Handlers.Commands
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreateRoomResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateRoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRoomDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateRoomDto);

            var response = new CreateRoomResponse();
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            // Check if the roomName already exists
            bool roomExists = await _unitOfWork.RoomRepository.CheckRoomNameExists(request.CreateRoomDto.CompanyId, request.CreateRoomDto.RoomName);

            if (roomExists)
            {
                response.Success = false;
                response.Message = "Room with the same name already exists.";
                return response;
            }

            var room = _mapper.Map<Room>(request.CreateRoomDto);
            room = await _unitOfWork.RoomRepository.AddAsync(room);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateRoomDto>(room);

            return response;
        }
    }
}
