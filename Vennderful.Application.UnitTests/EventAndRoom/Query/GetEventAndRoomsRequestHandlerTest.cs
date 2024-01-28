using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventAndRooms.Handlers.Queries;
using Vennderful.Application.Features.EventAndRooms.Requests;
using Vennderful.Application.Features.EventAndRooms.Responses;
using Vennderful.Application.Features.RateStructure.Handlers.Queries;
using Vennderful.Application.Features.RateStructure.Requests;
using Vennderful.Application.Features.RateStructure.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventAndRoom.Query
{
    public class GetEventAndRoomsRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventAndRoomRepository;

        public GetEventAndRoomsRequestHandlerTest()
        {
            _mockEventAndRoomRepository = RepositoryMocks.GetEventAndRoomRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetEventAndRoomsTest()
        {
            var handler = new GetEventAndRoomsRequestHandler(_mockEventAndRoomRepository.Object, _mapper);
            var result = await handler.Handle(new GetEventAndRoomsRequest(), CancellationToken.None);

            result.ShouldBeOfType<GetEventAndRoomsResponse>();
            result.Data.Count.ShouldBe(2);
        }
    }
}
