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
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.UnitTests.Features.Stripe.Handlers.Commands
{
    public class AddStripeCustomerCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockCustomerRepository;
        private readonly IMapper _mockMapper;
        private readonly Mock<IPaymentServices> _mockPaymentServices;
        private readonly AddStripeCustomerDTO _addStripeCustomerDTO;
        private readonly AddStripeCustomerCommandHandler _addStripeCustomerCommandHandler;
        private readonly StripeCustomerDTO _stripeCustomerDTO;

        public AddStripeCustomerCommandHandlerTests()
        {
            _mockCustomerRepository = RepositoryMocks.GetCustomerRepository();

            _mockPaymentServices = new Mock<IPaymentServices>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mockMapper = mapperConfig.CreateMapper();

            _addStripeCustomerCommandHandler = new AddStripeCustomerCommandHandler(_mockCustomerRepository.Object, _mockMapper, _mockPaymentServices.Object);

            _addStripeCustomerDTO = new AddStripeCustomerDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = new Address
                    (
                        "street1",
                        "city1",
                        "state1",
                        "country1",
                        "zip1"
                    ),
                CustomerType = 0,
                CreditLimit = 1000,
                CreditCard = new AddStripeCardDTO
                    (
                        "John Doe",
                        "4242424242424242",
                        "2030",
                        "01",
                        "123"
                    ),
            };

            _stripeCustomerDTO = new StripeCustomerDTO
            (
                "Cust1 Name",
                "customer1@vennderful.com",
                "cus_NZx49i7wtwcymN"
            );
        }

        [Fact]
        public async Task Should_Create_Customer()
        {
            // Arrange
            var request = new AddStripeCustomerCommand
            {
                AddStripeCustomerDTO = _addStripeCustomerDTO
            };
            var customerId = Guid.NewGuid();
            var validator = new AddStripeCustomerDTOValidator();
            var validationResult = await validator.ValidateAsync(request.AddStripeCustomerDTO);
            _mockPaymentServices.Setup(x => x.AddStripeCustomerAsync(request.AddStripeCustomerDTO, CancellationToken.None))
                .ReturnsAsync(_stripeCustomerDTO);
            _mockCustomerRepository.Setup(x => x.CustomerRepository.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Customer { Id = customerId, Name = request.AddStripeCustomerDTO.Name });
            _mockCustomerRepository.Setup(x => x.Save());

            // Act
            var response = await _addStripeCustomerCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<AddStripeCustomerResponse>();
            response.Success.ShouldBeTrue();
            response.Message.ShouldBe("Created Successfully.");
            _mockPaymentServices.Verify(x => x.AddStripeCustomerAsync(request.AddStripeCustomerDTO, CancellationToken.None), Times.Once);
            _mockCustomerRepository.Verify(x => x.CustomerRepository.AddAsync(It.IsAny<Customer>()), Times.Once);
            _mockCustomerRepository.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Create_Customer()
        {
            _addStripeCustomerDTO.Name = null;
            var result = await _addStripeCustomerCommandHandler.Handle(new AddStripeCustomerCommand() { AddStripeCustomerDTO = _addStripeCustomerDTO }
                , CancellationToken.None);

            var customers = await _mockCustomerRepository.Object.CustomerRepository.GetAllAsync();

            result.ShouldBeOfType<AddStripeCustomerResponse>();
            result.Success.ShouldBeFalse();
            result.Errors[0].ShouldBe("Name is required.");
            customers.Count.ShouldBe(2);
        }
    }
}