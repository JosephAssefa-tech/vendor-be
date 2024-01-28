using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Payment;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Application.Features.Stripe.Handlers.Commands;
using Vennderful.Application.Features.Stripe.Requests;
using Vennderful.Application.Features.Stripe.Responses;
using Vennderful.Application.Features.Stripe.Validators;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.UnitTests.Payments.Commands
{
    public class AddStripePaymentCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockPaymentRepository;
        private readonly IMapper _mockMapper;
        private readonly Mock<IPaymentServices> _mockPaymentServices;
        private readonly AddStripePaymentDTO _addStripePaymentDTO;
        private readonly AddStripePaymentCommandHandler _addStripePaymentCommandHandler;
        private readonly StripePaymentDTO _stripePaymentDTO;

        public AddStripePaymentCommandHandlerTests()
        {
            _mockPaymentRepository = RepositoryMocks.GetPaymentRepository();

            _mockPaymentServices = new Mock<IPaymentServices>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mockMapper = mapperConfig.CreateMapper();

            _addStripePaymentCommandHandler = new AddStripePaymentCommandHandler(_mockPaymentRepository.Object, _mockMapper, _mockPaymentServices.Object);

            _addStripePaymentDTO = new AddStripePaymentDTO
            {
                CustomerId = "1",
                ReceiptEmail = "john.doe@example.com",
                Description = "This is a sample description.",
                Currency = "USD",
                Amount = 1000,
            };

            _stripePaymentDTO = new StripePaymentDTO
            (
                "1",
                "john.doe@example.com",
                "This is a sample description.",
                "USD",
                1000,
                "1"
            );
        }

        [Fact]
        public async Task Should_Create_Payment()
        {
            // Arrange
            var request = new AddStripePaymentCommand
            {
                AddStripePaymentDTO = _addStripePaymentDTO
            };
            var validator = new AddStripePaymentDTOValidator();
            var validationResult = await validator.ValidateAsync(request.AddStripePaymentDTO);
            _mockPaymentServices.Setup(x => x.AddStripePaymentAsync(request.AddStripePaymentDTO, CancellationToken.None))
                .ReturnsAsync(_stripePaymentDTO);
            _mockPaymentRepository.Setup(x => x.PaymentRepository.AddAsync(It.IsAny<Payment>()))
                .ReturnsAsync(new Payment { CustomerId = request.AddStripePaymentDTO.CustomerId, ReceiptEmail = request.AddStripePaymentDTO.ReceiptEmail });
            _mockPaymentRepository.Setup(x => x.Save());

            // Act
            var response = await _addStripePaymentCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<AddStripePaymentResponse>();
            response.Success.ShouldBeTrue();
            response.Message.ShouldBe("Payment Successful.");
            _mockPaymentServices.Verify(x => x.AddStripePaymentAsync(request.AddStripePaymentDTO, CancellationToken.None), Times.Once);
            _mockPaymentRepository.Verify(x => x.PaymentRepository.AddAsync(It.IsAny<Payment>()), Times.Once);
            _mockPaymentRepository.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Create_Payment()
        {
            _addStripePaymentDTO.CustomerId = null;
            var result = await _addStripePaymentCommandHandler.Handle(new AddStripePaymentCommand() { AddStripePaymentDTO = _addStripePaymentDTO }
                , CancellationToken.None);

            var payments = await _mockPaymentRepository.Object.PaymentRepository.GetAllAsync();

            result.ShouldBeOfType<AddStripePaymentResponse>();
            result.Success.ShouldBeFalse();
            result.Errors[0].ShouldBe("Customer Id is required.");
            payments.Count.ShouldBe(2);
        }
    }
}
