using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventDocumentSigners.Handlers.Commands;
using Vennderful.Application.Features.EventDocumentSigners.Requests;
using Vennderful.Application.Features.EventDocumentSigners.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventDocumentSigner.Commands
{
    public class UpdateEventDocumentSignerCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventDocumentSignerRepository;
        private readonly UpdateEventDocumentSignerCommandHandler _updateEventDocumentSignerCommandHandler;

        public UpdateEventDocumentSignerCommandHandlerTests()
        {
            var notification = new Vennderful.Domain.Entities.Notification
            {
                Id = Guid.NewGuid(),
                HasBeenRead = false,
            };
            _mockEventDocumentSignerRepository = RepositoryMocks.GetEventDocumentSignerRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _updateEventDocumentSignerCommandHandler = new UpdateEventDocumentSignerCommandHandler(_mockEventDocumentSignerRepository.Object, _mapper);

        }

        [Fact]
        public async Task Should_Update_EventDocumentSigner()
        {
            var result = await _updateEventDocumentSignerCommandHandler.Handle(new UpdateEventDocumentSignerCommand() 
            {
                DocumentId = Guid.NewGuid(),
                SignerId = Guid.Parse("a8ca876b-585e-475a-9caf-ab06c33da91e"),
                EventDocumentId = Guid.Parse("609462fb-9f1b-4e83-842f-003051110d6b"),
            }
                , CancellationToken.None);

            result.ShouldBeOfType<UpdateEventDocumentSignerResponse>();
            result.Success.ShouldBeTrue();

        }


    }
}
