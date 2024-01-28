using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Notifications.Handlers.Commands;
using Vennderful.Application.Features.Notifications.Requests;
using Vennderful.Application.Features.Notifications.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Notification.Commands
{
    public class UpdateNotificationCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockNotificationRepository;
        private readonly UpdateNotificationCommandHandler _updateNotificationCommandHandler;

        public UpdateNotificationCommandHandlerTests()
        {
            var notification = new Vennderful.Domain.Entities.Notification
            {
                Id = Guid.NewGuid(),
                HasBeenRead = false,
            };
            _mockNotificationRepository = RepositoryMocks.UpdateNotificationRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _updateNotificationCommandHandler = new UpdateNotificationCommandHandler(_mockNotificationRepository.Object, _mapper);

        }

        [Fact]
        public async Task Should_Update_Notification()
        {
            var notificationId = Guid.NewGuid();
            var result = await _updateNotificationCommandHandler.Handle(new UpdateNotificationCommand() { Id = notificationId }
                , CancellationToken.None);
            
            result.IsRead.ShouldBeTrue();
            result.ShouldBeOfType<UpdateNotificationResponse>();
            result.Success.ShouldBeTrue();
            
        }

        
    }
}
