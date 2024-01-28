using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Events.Handlers.Queries;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.UnitTests.Event.Queries
{
    public class GetEventClientsRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventClientsRepository;

        public GetEventClientsRequestHandlerTest()
        {
            _mockEventClientsRepository = RepositoryMocks.GetEventClientsRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Return_Correct_Response()
        {
            var eventId = Guid.Parse("237f3d47-76c0-4ac2-8116-535156342317");

            var eventClients = new List<EventClient>
        {
            new EventClient
            {
                EventId = eventId,
                Client = new Domain.Entities.Client
                {
                    Id = Guid.Parse("11607680-1cdf-46b0-8630-7ec0c60d2a2b"),
                    FirstName = "John",
                    LastName = "Doe",
                    // Add other properties as needed
                }
            },
            new EventClient
            {
                EventId = eventId,
                Client = new Domain.Entities.Client
                {
                    Id = Guid.Parse("369e65a0-e00b-4816-89c8-ff2395543626"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    // Add other properties as needed
                }
            }
        };

            _mockEventClientsRepository.Setup(repo => repo.eventClientRepository.GetEventClientsByEventId(eventId))
                .ReturnsAsync(eventClients);

            var handler = new GetEventClientsRequestHandler(_mockEventClientsRepository.Object, _mapper);

            var request = new GetEventClientsRequest { Id = eventId };
            var response = await handler.Handle(request, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeOfType<GetEventClentsResponse>();
            response.Success.ShouldBeTrue();
            response.Data.ShouldNotBeNull();
            response.Data.ShouldBeOfType<List<ListClientDTO>>();
            response.Data.Count.ShouldBe(eventClients.Count);

            for (int i = 0; i < response.Data.Count; i++)
            {
                var eventClient = eventClients[i];
                var clientDto = response.Data[i];
                clientDto.Id.ShouldBe(eventClient.Client.Id);
                clientDto.FirstName.ShouldBe(eventClient.Client.FirstName);
                clientDto.LastName.ShouldBe(eventClient.Client.LastName);
                // Assert other properties as needed
            }
        }
        [Fact]
        public async Task GetEventClientsByEventId_Should_Return_Result_Test()
        {
            // Arrange
            List<EventClient> eventClients = new List<EventClient>(); 
            _mockEventClientsRepository.Setup(repo => repo.eventClientRepository.GetEventClientsByEventId(It.IsAny<Guid>()))
                .ReturnsAsync(eventClients);

            var handler = new GetEventClientsRequestHandler(_mockEventClientsRepository.Object, _mapper);
            var request = new GetEventClientsRequest { Id = Guid.Parse("237f3d47-76c0-4ac2-8116-535156342311") };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<GetEventClentsResponse>();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("eventClients Not Found.");
            result.Data.ShouldBeNull();
        }
    }

}
