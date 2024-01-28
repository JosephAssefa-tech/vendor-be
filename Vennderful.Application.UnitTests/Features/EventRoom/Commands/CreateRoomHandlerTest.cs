using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.EventRoom.Handlers.Commands;
using Vennderful.Application.Features.EventRoom.Requests;
using Vennderful.Application.Features.EventRoom.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Features.EventRoom.Commands
{
    public class CreateRoomHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockVenueRepository;
        CreateRoomDto _createRoomDto;
        CreateRoomCommandHandler _createRoomCommandHandler;
        public CreateRoomHandlerTest()
        {
            _mockVenueRepository = RepositoryMocks.GetRoomRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createRoomCommandHandler = new CreateRoomCommandHandler(_mockVenueRepository.Object, _mapper);

            _createRoomDto = new CreateRoomDto
            {
                Id = Guid.NewGuid(),
                RoomName = "roomA",

            };
        }
        [Fact]
        public async Task Should_Create_Room()
        {
            var result = await _createRoomCommandHandler.Handle(new CreateRoomCommand() { CreateRoomDto = _createRoomDto }
                , CancellationToken.None);

            var venuss = await _mockVenueRepository.Object.RoomRepository.GetAllAsync();

            result.ShouldBeOfType<CreateRoomResponse>();
            result.Success.ShouldBeTrue();
            venuss.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Should_Not_Create_Room()
        {
            _createRoomDto.RoomName = null;
            var result = await _createRoomCommandHandler.Handle(new CreateRoomCommand() { CreateRoomDto = _createRoomDto }
                , CancellationToken.None);

            var venus = await _mockVenueRepository.Object.RoomRepository.GetAllAsync();

            result.ShouldBeOfType<CreateRoomResponse>();
            result.Success.ShouldBeFalse();
            venus.Count.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createRoomDto.RoomName = string.Empty;
            var result = await _createRoomCommandHandler.Handle(new CreateRoomCommand() { CreateRoomDto = _createRoomDto }
                , CancellationToken.None);

            var venus = await _mockVenueRepository.Object.RoomRepository.GetAllAsync();

            result.Errors[0].ShouldBe("{RoomName} is required.");
        }
    }
}
