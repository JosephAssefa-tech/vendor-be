using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventDocuments.Dto;
using Vennderful.Application.Features.EventDocuments.Handlers.Commands;
using Vennderful.Application.Features.EventDocuments.Requests;
using Vennderful.Application.Features.EventDocuments.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.UnitTests.Features.EventDocument.Command
{
    public class CreateEventDocumentCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventDocumentRepository;
        CreateEventDocumentsDto _createEventDocumenteDTO;
        CreateEventDocumentsDto _createEventDocumentFailDTO;
        CreateEventDocumentCommandHanler _createEventDocumentCommandHandler;
        public CreateEventDocumentCommandHandlerTest()
        {
            _mockEventDocumentRepository = RepositoryMocks.GetEventDocumentRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventDocumentCommandHandler = new CreateEventDocumentCommandHanler(_mockEventDocumentRepository.Object, _mapper);

            _createEventDocumenteDTO = new CreateEventDocumentsDto
            {
                EventId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DocumentId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DocumentSignerType = (DocumentSignerType)1,


            };
            _createEventDocumentFailDTO = new CreateEventDocumentsDto
            {
                DocumentId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DocumentSignerType = (DocumentSignerType)1,

            };
        }
        [Fact]
        public async Task Should_Create_Event_Document()
        {
         
            var result = await _createEventDocumentCommandHandler.Handle(new CreateEventDocumentCommand() { CreateEventDocument = _createEventDocumenteDTO }
                , CancellationToken.None);

            var customers = await _mockEventDocumentRepository.Object.eventDocumentRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventDocumentResponse>();
            result.Success.ShouldBeTrue();
            customers.Count.ShouldBe(1);
        }
        [Fact]
        public async Task Should_Not_Create_Event_Document()
        {

            var result = await _createEventDocumentCommandHandler.Handle(new CreateEventDocumentCommand() { CreateEventDocument = _createEventDocumentFailDTO }
                , CancellationToken.None);

            var customers = await _mockEventDocumentRepository.Object.eventDocumentRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventDocumentResponse>();
            result.Success.ShouldBeFalse();
        }
    }
}
