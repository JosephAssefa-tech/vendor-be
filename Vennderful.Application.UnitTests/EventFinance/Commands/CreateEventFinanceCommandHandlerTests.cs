using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Handlers.Commands;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventFinance.Commands
{
    public class CreateEventFinanceCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventFinanceRepository;
        private readonly CreateEventFinanceDto _createEventFinanceDTO;
        private readonly CreateEventFinanceCommandHandler _createEventFinanceCommandHandler;

        public CreateEventFinanceCommandHandlerTests()
        {
            _mockEventFinanceRepository = RepositoryMocks.GetEventFinanceRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventFinanceCommandHandler = new CreateEventFinanceCommandHandler(_mockEventFinanceRepository.Object, _mapper);

            _createEventFinanceDTO = new CreateEventFinanceDto
            {
                EventId = Guid.NewGuid(),
                PackageId = Guid.NewGuid(),
                DepositAmount = 300,
                TravelFees = 400,
                Addons = new List<Domain.Entities.Addon> { new Domain.Entities.Addon
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        TotalPrice = 0,
                    } 
                },
                Payments = new List<PaymentSchedules> { new PaymentSchedules
                {
                    Id = 1,
                    PaymentDate = DateTime.UtcNow,
                    ScheduleAmount = 1,
                } }
            };
        }

        [Fact]
        public async Task Should_Create_EventFinance()
        {
            var result = await _createEventFinanceCommandHandler.Handle(new CreateEventFinanceCommand() { CreateEventFinanceDto = _createEventFinanceDTO }
                , CancellationToken.None);

            var eventFinances = await _mockEventFinanceRepository.Object.eventFinanceRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventFinanceResponse>();
            result.Success.ShouldBeTrue();
            eventFinances.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Should_Not_Create_EventFinance()
        {
            _createEventFinanceDTO.EventId = Guid.Empty;
            var result = await _createEventFinanceCommandHandler.Handle(new CreateEventFinanceCommand() { CreateEventFinanceDto = _createEventFinanceDTO }
                , CancellationToken.None);

            var customers = await _mockEventFinanceRepository.Object.eventFinanceRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventFinanceResponse>();
            result.Success.ShouldBeFalse();
            customers.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createEventFinanceDTO.EventId = Guid.Empty;
            var result = await _createEventFinanceCommandHandler.Handle(new CreateEventFinanceCommand() { CreateEventFinanceDto = _createEventFinanceDTO }
                , CancellationToken.None);

            var customers = await _mockEventFinanceRepository.Object.eventFinanceRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Event Id is required.");
        }
    }
}
