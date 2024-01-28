using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Events.Handlers.Queries;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.User.Handlers.Queries;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Event.Queries
{
    public class GetEventRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventRepository;

        public GetEventRequestHandlerTests()
        {
            _mockEventRepository = RepositoryMocks.GetEventRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Should_Not_Get_Event_Failed()
        {
            var handler = new GetEventRequestHandler(_mockEventRepository.Object, _mapper);
            var result = await handler.Handle(new GetEventByIdRequest() { Id = Guid.Parse("bf2a648e-e68b-4153-98f5-bfaf512c1953"), CompanyId = Guid.Parse("78398392-0279-4805-bdd8-438bbb6d6324") }, CancellationToken.None);

            result.ShouldBeOfType<GetEventByIdResponse>();
            result.Success.ShouldBeFalse();
        }
    }
}
