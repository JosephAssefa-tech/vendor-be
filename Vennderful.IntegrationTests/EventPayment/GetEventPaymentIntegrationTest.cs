using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Vennderful.API.Controllers;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.EventPayment.Responses;
using Vennderful.Domain.Enums;

namespace Vennderful.IntegrationTests.EventPayment
{
    public class GetEventPaymentIntegrationTest
    {
        Mock<IMediator> mockMediator = new Mock<IMediator>();

        public GetEventPaymentIntegrationTest()
        {
        }

        [Fact]
        public async Task GetEventPayment_ReturnsSuccessStatusCode()
        {
            var controller = new EventController(mockMediator.Object);
            var getEventPaymentRequest = new EventPaymentDTO
            {
                EventId = Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1cf")
            };
            var getEventPaymentResponse = new List<EventPaymentDTO>()
            {
                new EventPaymentDTO { 
                    ClientId = Guid.Empty, 
                    EventId = Guid.Empty, 
                    PaymentAmount = 0, 
                    PaymentDate = DateTime.UtcNow,
                    PaymentMethod = "Cash",
                    PaymentNote = "",
                    PaymentReason = ""
                }
            };

            var mockResponse = new GetEventPaymentsResponse() { Data = getEventPaymentResponse };

            mockMediator
                .Setup(m => m.Send(getEventPaymentRequest, default))
                .ReturnsAsync(mockResponse);

            // Act
            var response = await controller.GetEventPayments(Guid.Empty);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var okObjectResult = response.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

        }
    }
}
