using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.EventFinance.Handlers.Queries;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventFinance.Queries
{
    public class GetEventFinanceHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventFinanceRepository;

        public GetEventFinanceHandlerTest()
        {
            _mockEventFinanceRepository = RepositoryMocks.GetEventFinanceRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetEventFinanceByEventId_Should_Return_Result_Test()
        {
            var handler = new GetEventFinanceRequestHandler(_mockEventFinanceRepository.Object, _mapper);
            var result = await handler.Handle(new GetEventFinanceRequest() { EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") }, CancellationToken.None);

            result.ShouldBeOfType<GetEventFinanceResponse>();
            result.Data.EventId.ShouldBe(Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d"));
        }
    }
}
