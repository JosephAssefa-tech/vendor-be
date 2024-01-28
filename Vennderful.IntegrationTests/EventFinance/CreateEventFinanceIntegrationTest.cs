using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Vennderful.API.Controllers;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Responses;

namespace Vennderful.IntegrationTests.EventFinance
{
    public class CreateEventFinanceIntegrationTest
    {
        Mock<IMediator> mockMediator = new Mock<IMediator>();

        public CreateEventFinanceIntegrationTest()
        {
        }

        [Fact]
        public async Task CreateEventFinance_ReturnsSuccessStatusCode()
        {
            var controller = new EventController(mockMediator.Object);
            var createEventFinanceDto = new CreateEventFinanceDto() 
            {
                EventId = Guid.Empty,
                PackageId = Guid.NewGuid(),
                DepositAmount = 100,
                TravelFees = 200
            };
            var mockResponse = new CreateEventFinanceResponse() { Data = createEventFinanceDto };

            mockMediator
                .Setup(m => m.Send(createEventFinanceDto, default))
                .ReturnsAsync(mockResponse);

            // Act
            var response = await controller.CreateEventFinance(Guid.Empty, createEventFinanceDto);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<CreatedResult>(response.Result);

            var createdResult = response.Result as CreatedResult;
            Assert.NotNull(createdResult);

            var resultData = createdResult.Value as CreateEventFinanceResponse;
        }
    }
}
