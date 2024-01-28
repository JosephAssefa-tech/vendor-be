using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Vennderful.API.Controllers;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Responses;

namespace Vennderful.IntegrationTests.EventFinance
{
    public class GetEventFinanceIntegrationTest
    {
        Mock<IMediator> mockMediator = new Mock<IMediator>();

        public GetEventFinanceIntegrationTest()
        {
        }

        [Fact]
        public async Task GetEventFinance_ReturnsSuccessStatusCode()
        {
            var controller = new EventController(mockMediator.Object);
            var getEventFinanceDto = new CreateEventFinanceDto()
            {
                EventId = Guid.Empty,
                PackageId = Guid.NewGuid(),
                DepositAmount = 100,
                TravelFees = 200
            };
            var mockResponse = new CreateEventFinanceResponse() { Data = getEventFinanceDto };

            mockMediator
                .Setup(m => m.Send(getEventFinanceDto, default))
                .ReturnsAsync(mockResponse);

            // Act
            var response = await controller.GetEventFinance(Guid.Empty);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var okObjectResult = response.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

        }
    }
}
