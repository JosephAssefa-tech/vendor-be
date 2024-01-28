using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventAndMember.DTO;
using Vennderful.Application.Features.EventAndMember.Handler.Queries;
using Vennderful.Application.Features.EventAndMember.Requests;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventAndMember.Queries
{
    public class GetEventAndMembersQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventAndMembersRepository;

        public GetEventAndMembersQueryHandlerTest()
        {
            _mockEventAndMembersRepository = RepositoryMocks.GetEventAndMembersRepository();

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
            var request = new GetEventAndMembersRequest { EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") };
            var cancellationToken = CancellationToken.None;

            // Act
            var handler = new GetEventAndMembersQueryHandler(_mockEventAndMembersRepository.Object, _mapper);
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeTrue();
            response.Data.ShouldBeOfType<List<ListEventAndMembersDTO>>();
        }

        [Fact]
        public async Task Handle_ExceptionThrown_ReturnsErrorResponseWithMessageAndEmptyData()
        {
            // Arrange
            var request = new GetEventAndMembersRequest { EventId = Guid.Empty };
            var cancellationToken = CancellationToken.None;

            // Simulate an exception being thrown
            _mockEventAndMembersRepository.Setup(repo => repo.eventAndMemberRepository.GetMembersByEventId(request.EventId))
                .ThrowsAsync(new Exception("An error occurred while getting event members."));

            // Act
            var handler = new GetEventAndMembersQueryHandler(_mockEventAndMembersRepository.Object, _mapper);
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
