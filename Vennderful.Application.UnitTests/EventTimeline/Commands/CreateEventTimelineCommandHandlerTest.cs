using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventTimeline.DTOs;
using Vennderful.Application.Features.EventTimeline.Handlers.Commands;
using Vennderful.Application.Features.EventTimeline.Requests;
using Vennderful.Application.Features.EventTimeline.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.UnitTests.EventTimeline.Commands
{
    public class CreateEventTimelineCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventTimelineRepository;
        private readonly CreateEventTimelineDto _createEventTimelineDTO;
        private readonly CreateEventTimelineCommandHandler _createEventTimelineCommandHandler;

        public CreateEventTimelineCommandHandlerTest()
        {
            _mockEventTimelineRepository = RepositoryMocks.GetEventTimelineRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventTimelineCommandHandler = new CreateEventTimelineCommandHandler(_mockEventTimelineRepository.Object, _mapper);

            _createEventTimelineDTO = new CreateEventTimelineDto
            {
                SlotTitle = "Slot 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                StartTime = "22",
                EndTime = "11",
                Comment = "payment note",
                ResponsiblePersons = new List<ResponsiblePerson>
                {
                    new ResponsiblePerson { Id = Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1ce"), Type = "Client" },
                    new ResponsiblePerson { Id = Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058aace"), Type = "Music" }
                }
            };
        }

        [Fact]
        public async Task Should_Create_EventTimeline()
        {
            var result = await _createEventTimelineCommandHandler.Handle(new CreateEventTimelineCommand() { CreateEventTimelineDto = _createEventTimelineDTO }
                , CancellationToken.None);

            var eventTimeline = await _mockEventTimelineRepository.Object.eventTimelineRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventTimelineResponse>();
            result.Success.ShouldBeTrue();
        }
    }
}
