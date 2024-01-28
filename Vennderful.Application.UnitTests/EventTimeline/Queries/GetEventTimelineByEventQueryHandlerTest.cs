using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventTimeline.DTOs;
using Vennderful.Application.Features.EventTimeline.Handlers.Queries;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventTimeline.Queries
{
    public class GetEventTimelineByEventQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventTimelineRepository;

        public GetEventTimelineByEventQueryHandlerTest()
        {
            _mockEventTimelineRepository = RepositoryMocks.GetEventTimelineRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResponseWithData()
        {
            // Arrange
            var request = new GetEventTimelinesRequest { EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") };
            var cancellationToken = CancellationToken.None;

            // Act
            var handler = new GetEventTimelinesRequestHandler(_mockEventTimelineRepository.Object, _mapper);
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeTrue();
            response.Data.ShouldBeOfType<List<EventTimelineDto>>();
        }

        [Fact]
        public async Task Handle_ExceptionThrown_ReturnsErrorResponseWithMessageAndEmptyData()
        {
            // Arrange
            var request = new GetEventTimelinesRequest { EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") };
            var cancellationToken = CancellationToken.None;

            // Simulate an exception being thrown
            _mockEventTimelineRepository.Setup(repo => repo.eventTimelineRepository.GetEventTimelineByEventId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("An error occurred while getting event payments."));

            // Act
            var handler = new GetEventTimelinesRequestHandler(_mockEventTimelineRepository.Object, _mapper);
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeFalse();
            response.Message.ShouldBe("Something went wrong.");
            response.Data.ShouldBeEmpty();
            response.Errors.ShouldNotBeEmpty();
        }
    }
}
