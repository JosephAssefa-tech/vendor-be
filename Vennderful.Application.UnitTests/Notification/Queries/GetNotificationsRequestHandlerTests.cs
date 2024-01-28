using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Notifications.Handlers.Queries;
using Vennderful.Application.Features.Notifications.Requests;
using Vennderful.Application.Features.Notifications.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Notification.Queries
{
    public class GetNotificationsRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockNotificationsRepository;

        public GetNotificationsRequestHandlerTests()
        {
            _mockNotificationsRepository = RepositoryMocks.GetNotificationRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Return_Correct_Response()
        {
            var handler = new GetNotificationsRequestHandler(_mockNotificationsRepository.Object, _mapper);

            var request = new GetNotificationsRequest { UserId = Guid.Empty };
            var response = await handler.Handle(request, CancellationToken.None);

            response.Data.ShouldNotBeNull();
            response.ShouldBeOfType<GetNotificationsResponse>();
            response.Success.ShouldBeTrue();
            response.Data.Count.ShouldBe(2);

        }
        [Fact]
        public async Task Handle_Should_Return_Not_Found()
        {
            var handler = new GetNotificationsRequestHandler(_mockNotificationsRepository.Object, _mapper);

            var request = new GetNotificationsRequest { UserId = Guid.NewGuid() };
            var response = await handler.Handle(request, CancellationToken.None);

            response.ShouldBeOfType<GetNotificationsResponse>();
            response.Data.ShouldBeNull();
            response.Message.ShouldBe("Any Notification Not Found.");
        }
    }

}
