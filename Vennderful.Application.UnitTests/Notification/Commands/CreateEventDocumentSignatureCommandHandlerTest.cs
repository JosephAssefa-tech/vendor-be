using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Handlers.Commands;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.UnitTests.Notification.Commands
{
    public class CreateEventDocumentSignatureCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockNotificationRepository;
        CreateEventDocumentSignatureNotificationDto _createEventDocumentSignerDto;
        CreateEventDocumentSignatureNotificationDto _dont_createEventDocumentSignerDto;
        private readonly CreateEventDocumentSignatureHandler _createEventDocumentSignatureHandler;

        public CreateEventDocumentSignatureCommandHandlerTest()
        {
            var notification = new Vennderful.Domain.Entities.Notification
            {
                Id = Guid.NewGuid(),
                HasBeenRead = false,
            };
            _mockNotificationRepository = RepositoryMocks.CreateEventDocumentSignerNotificationRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventDocumentSignatureHandler = new CreateEventDocumentSignatureHandler(_mockNotificationRepository.Object, _mapper);

            _createEventDocumentSignerDto = new CreateEventDocumentSignatureNotificationDto
            {
                UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                NotificationType = (NotificationType)1,
                NotificationMethod = (NotificationMethod)1,
                Content = "mock content",
                ClientId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                EventId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                EventDocumentId = Guid.Parse("365baaf8-c774-4739-b175-a15a2fe9f1ce"),
                DocumentId = Guid.Parse("03f35fc3-8065-41cc-a832-086808f8e2f2"),
                SenderId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                HasBeenRead = true
            };

            _dont_createEventDocumentSignerDto = new CreateEventDocumentSignatureNotificationDto
            {
                UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                NotificationType = (NotificationType)1,
                NotificationMethod = (NotificationMethod)1,
                Content = "mock content",
                ClientId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                EventDocumentId = Guid.Parse("365baaf8-c774-4739-b175-a15a2fe9f1ce"),
                DocumentId = Guid.Parse("03f35fc3-8065-41cc-a832-086808f8e2f2"),
                SenderId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                HasBeenRead = true
            };
        }
        [Fact]
        public async Task Should_Create_Event_Document_Signer_Notification()
        {
            var result = await _createEventDocumentSignatureHandler.Handle(new CreateEventDocumentSignatureNotificationCommand() { CreateEventDocumentSignatureDto = _createEventDocumentSignerDto }
                , CancellationToken.None);

            var venuss = await _mockNotificationRepository.Object.notificationRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventDocumentSignatureNotificationResponse>();
            result.Success.ShouldBeTrue();
            venuss.Count.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Not_Create_Event_Document_Signer_Notification()
        {
            var result = await _createEventDocumentSignatureHandler.Handle(new CreateEventDocumentSignatureNotificationCommand() { CreateEventDocumentSignatureDto = _dont_createEventDocumentSignerDto }
                , CancellationToken.None);

            var venuss = await _mockNotificationRepository.Object.notificationRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventDocumentSignatureNotificationResponse>();
            result.Success.ShouldBeFalse();
        }

    }
}
