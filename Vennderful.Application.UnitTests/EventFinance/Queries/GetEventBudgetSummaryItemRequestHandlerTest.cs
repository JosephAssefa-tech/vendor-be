using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventFinance.Handlers.Queries;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventFinance.Queries
{
    public class GetEventBudgetSummaryItemRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventFinanceBudgetSummaryItemRepository;

        public GetEventBudgetSummaryItemRequestHandlerTest()
        {
            _mockEventFinanceBudgetSummaryItemRepository = RepositoryMocks.GetEventFinanceBudgetSummaryItemRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetEventFinanceByEventId_Should_Return_Result_Test()
        {
            // Arrange
            var handler = new GetEventBudgetSummaryItemRequestHandler(_mockEventFinanceBudgetSummaryItemRepository.Object, _mapper);
            var request = new GetEventBudgetSummaryItemRequest
            {
                EventId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                PackageId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
              AddonId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2")
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<GetEventBudgetSummaryItemResponse>();
            result.Data.ShouldNotBeNull();
            var eventFinanceBudgetSummaryItem = result.Data;

            // Assert the properties of eventFinanceBudgetSummaryItem
            eventFinanceBudgetSummaryItem.PackageName.ShouldBe("PackageA");
            eventFinanceBudgetSummaryItem.AddonName.ShouldBe("addonA");
            eventFinanceBudgetSummaryItem.AddonTotalPrice.ShouldBeGreaterThan(0);
        }
        [Fact]
        public async Task GetEventFinanceByEventId_Should_Return_Result_Null()
        {
            // Arrange
            var handler = new GetEventBudgetSummaryItemRequestHandler(_mockEventFinanceBudgetSummaryItemRepository.Object, _mapper);
            var request = new GetEventBudgetSummaryItemRequest
            {
                EventId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e1"),
                PackageId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e1"),
                AddonId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e1")
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<GetEventBudgetSummaryItemResponse>();
            result.Data.ShouldBeNull();    

        }




    }
}
