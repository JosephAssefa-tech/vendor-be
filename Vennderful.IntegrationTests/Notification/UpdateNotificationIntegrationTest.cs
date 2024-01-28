using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Vennderful.API.Controllers;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Features.Notifications.Responses;

namespace Vennderful.IntegrationTests.Notification
{
    public class UpdateNotificationIntegrationTest
    {
        Mock<IMediator> mockMediator = new Mock<IMediator>();

        public UpdateNotificationIntegrationTest()
        {
        }

        [Fact]
        public async Task CreateEventFinance_ReturnsSuccessStatusCode()
        {
            var controller = new NotificationController(mockMediator.Object);
            
            var mockResponse = new UpdateNotificationResponse() { IsRead = true };

            mockMediator
                .Setup(m => m.Send(Guid.NewGuid(), default))
                .ReturnsAsync(mockResponse);

            // Act
            var response = await controller.UpdateNotification(Guid.Empty);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var okObjectResult = response.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultData = okObjectResult.Value as UpdateNotificationResponse;
        }
    }
}
