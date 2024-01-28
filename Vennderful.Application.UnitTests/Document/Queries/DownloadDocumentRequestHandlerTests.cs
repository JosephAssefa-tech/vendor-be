using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Interfaces;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.NewDocuments.Handlers.Queries;
using Vennderful.Application.Features.NewDocuments.Requests;
using Vennderful.Application.Features.NewDocuments.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Document.Queries
{
    public class DownloadDocumentRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockDownloadDocumentRepository;
        private readonly Mock<IFileService> _mockFileService;

        public DownloadDocumentRequestHandlerTests()
        {
            _mockDownloadDocumentRepository = RepositoryMocks.GetNewDocumentRepository();
            _mockFileService = new Mock<IFileService>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Should_Download_Document_Test()
        {
            var handler = new DownloadDocumentRequestHandler(_mockDownloadDocumentRepository.Object, _mapper, _mockFileService.Object);
            var result = await handler.Handle(new DownloadDocumentRequest()
            {
                Id = Guid.Parse("29de9d27-c23d-4633-b654-b1e4651fa5f8"),
            }, CancellationToken.None);

            result.ShouldBeOfType<DownloadDocumentResponse>();
            result.DocumentName.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Not_Download_Document_Test()
        {
            var handler = new DownloadDocumentRequestHandler(_mockDownloadDocumentRepository.Object, _mapper, _mockFileService.Object);
            var result = await handler.Handle(new DownloadDocumentRequest()
            {
                Id = Guid.NewGuid(),
            }, CancellationToken.None);

            result.ShouldBeOfType<DownloadDocumentResponse>();
        }
    }
}
