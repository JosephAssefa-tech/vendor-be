using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.EventPayment.Handlers.Commands;
using Vennderful.Application.Features.EventPayment.Requests;
using Vennderful.Application.Features.EventPayment.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventPayment.Commands
{
    public class CreateEventPaymentCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventPaymentRepository;
        private readonly CreateEventPaymentDTO _createEventPaymentDTO;
        private readonly CreateEventPaymentCommandHandler _createEventPaymentCommandHandler;

        public CreateEventPaymentCommandHandlerTest()
        {
            _mockEventPaymentRepository = RepositoryMocks.GetEventPaymentRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventPaymentCommandHandler = new CreateEventPaymentCommandHandler(_mockEventPaymentRepository.Object, _mapper);

            _createEventPaymentDTO = new CreateEventPaymentDTO
            {
                EventId = Guid.NewGuid(),
                ClientId= Guid.NewGuid(),
                PaymentMethod=Domain.Enums.PaymentMethod.Check,
                PaymentReason="Additional Payment",
                PaymentDate= DateTime.Now,
                PaymentAmount= 250.50M,
                PaymentNote= "payment note"
            };
        }

        [Fact]
        public async Task Should_Create_EventPayment()
        {
            var result = await _createEventPaymentCommandHandler.Handle(new CreateEventPaymentCommand() { CreateEventPaymentDTO = _createEventPaymentDTO }
                , CancellationToken.None);

            var eventPayments = await _mockEventPaymentRepository.Object.eventPaymentRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventPaymentResponse>();
            result.Success.ShouldBeTrue();
            eventPayments.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Not_Create_EventPayment()
        {
            _createEventPaymentDTO.EventId = Guid.Empty;
            var result = await _createEventPaymentCommandHandler.Handle(new CreateEventPaymentCommand() { CreateEventPaymentDTO = _createEventPaymentDTO }
                , CancellationToken.None);

            var payments = await _mockEventPaymentRepository.Object.eventPaymentRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventPaymentResponse>();
            result.Success.ShouldBeFalse();
            payments.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createEventPaymentDTO.EventId = Guid.Empty;
            var result = await _createEventPaymentCommandHandler.Handle(new CreateEventPaymentCommand() { CreateEventPaymentDTO = _createEventPaymentDTO }
                , CancellationToken.None);

            var payments = await _mockEventPaymentRepository.Object.eventPaymentRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Event Id is required.");
        }
    }
}
