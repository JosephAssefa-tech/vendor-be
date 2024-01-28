using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.EventPayment.Handlers.Queries;
using Vennderful.Application.Features.EventPayment.Requests;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventPayment.Queries
{
    public class GetEventPaymentsByEventCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventPaymentRepository;

        public GetEventPaymentsByEventCommandHandlerTest()
        {
            _mockEventPaymentRepository = RepositoryMocks.GetEventPaymentRepository();

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
            var request = new GetEventPaymentsRequest { EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") };
            var cancellationToken = CancellationToken.None;

            // Act
            var handler = new GetEventPaymentsRequestHandler(_mockEventPaymentRepository.Object, _mapper);
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeTrue();
            response.Data.ShouldBeOfType<List<EventPaymentDTO>>();
        }

        [Fact]
        public async Task Handle_ExceptionThrown_ReturnsErrorResponseWithMessageAndEmptyData()
        {
            // Arrange
            var request = new GetEventPaymentsRequest { EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") };
            var cancellationToken = CancellationToken.None;

            // Simulate an exception being thrown
            _mockEventPaymentRepository.Setup(repo => repo.eventPaymentRepository.GetEventPaymentsByEventId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("An error occurred while getting event payments."));

            // Act
            var handler = new GetEventPaymentsRequestHandler(_mockEventPaymentRepository.Object, _mapper);
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
