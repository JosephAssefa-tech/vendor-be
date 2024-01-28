using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Handlers.Commands;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Features.NewDocument.Commands
{
    public class CreateNewDocumentHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockNewDocumentRepository;
        CreateNewDocumentDto _createNewDocumentDto;
        CreateNewDocumentHandler _createNewDocumentCommandHandler;
        public CreateNewDocumentHandlerTest()
        {
            _mockNewDocumentRepository = RepositoryMocks.GetNewDocumentRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _createNewDocumentCommandHandler = new CreateNewDocumentHandler(_mockNewDocumentRepository.Object, _mapper);

            _createNewDocumentDto = new CreateNewDocumentDto
            {
                Id = new Guid("29de9d27-c23d-4633-b654-b1e4651fa5f8"),
                DocumentName = "doc A",
                DocumentBody = "body of doc A",
                DocumentCategory = 0,
                DocumentDescription = "Descritption for doc A",
   
            };

        }
        [Fact]
        public async Task Should_Create_New_Document()
        {
            var result = await _createNewDocumentCommandHandler.Handle(new CreateNewDocumentCommand() { CreateNewDocumentDto = _createNewDocumentDto }
                , CancellationToken.None);

            var venuss = await _mockNewDocumentRepository.Object.NewDocumentRepository.GetAllAsync();

            result.ShouldBeOfType<CountNewlAddedDocumentsResponse>();
            result.Success.ShouldBeTrue();
            venuss.Count.ShouldBe(3);
        }
        [Fact]
        public async Task Should_Not_Create_New_Document()
        {
            _createNewDocumentDto.DocumentName = null;
            var result = await _createNewDocumentCommandHandler.Handle(new CreateNewDocumentCommand() { CreateNewDocumentDto = _createNewDocumentDto }
                , CancellationToken.None);

            var venus = await _mockNewDocumentRepository.Object.NewDocumentRepository.GetAllAsync();

            result.ShouldBeOfType<CountNewlAddedDocumentsResponse>();
            result.Success.ShouldBeFalse();
            venus.Count.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createNewDocumentDto.DocumentName = string.Empty;
            var result = await _createNewDocumentCommandHandler.Handle(new CreateNewDocumentCommand() { CreateNewDocumentDto = _createNewDocumentDto }
                , CancellationToken.None);

            var venus = await _mockNewDocumentRepository.Object.NewDocumentRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Document Name is required.");
        }
    }
}
