using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Documents.Handlers.Commands;
using Vennderful.Application.Features.Documents.Requests;
using Vennderful.Application.Features.Documents.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Document.Commands
{
    public class UpdateDocumentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockDocumentRepository;
        private readonly EditDocumentCommandHandler _updateDocumentCommandHandler;

        public UpdateDocumentCommandHandlerTests()
        {
            var document = new Vennderful.Domain.Entities.Document
            {
                Id = Guid.NewGuid(),
            };
            _mockDocumentRepository = RepositoryMocks.UpdateDocumentRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _updateDocumentCommandHandler = new EditDocumentCommandHandler(_mockDocumentRepository.Object, _mapper);

        }

        [Fact]
        public async Task Should_Update_Document()
        {
            var editDocumentDto = new Application.Features.Documents.DTOs.EditDocumentDto()
            {
                Id = Guid.NewGuid(),
                DocumentName = ""
            };
            var result = await _updateDocumentCommandHandler.Handle(new EditDocumentCommand() {  EditDocumentDto = editDocumentDto }
                , CancellationToken.None);

            result.ShouldBeOfType<EditDocumentResponse>();
        }


    }
}
