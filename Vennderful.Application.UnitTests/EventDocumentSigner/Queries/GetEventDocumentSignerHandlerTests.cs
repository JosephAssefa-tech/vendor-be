using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventDocumentSigners.Handlers.Queries;
using Vennderful.Application.Features.EventDocumentSigners.Requests;
using Vennderful.Application.Features.EventDocumentSigners.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventDocumentSigner.Queries
{
    public class GetEventDocumentSignerHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventDocumentSignerRepository;

        public GetEventDocumentSignerHandlerTests()
        {
            _mockEventDocumentSignerRepository = RepositoryMocks.GetEventDocumentSignerRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetEventDocumentSigner_Should_Return_Result_Test()
        {
            var handler = new GetEventDocumentSignerRequestHandler(_mockEventDocumentSignerRepository.Object, _mapper);
            var result = await handler.Handle(new GetEventDocumentSignerRequest() { 
                DocumentId = Guid.Parse("ce52f6dc-a4f2-4dfe-be18-62926ca12a7f"), 
                EventDocumentId = Guid.Parse("609462fb-9f1b-4e83-842f-003051110d6b"),
                SignerId = Guid.Parse("a8ca876b-585e-475a-9caf-ab06c33da91e")
            }, CancellationToken.None);

            result.ShouldBeOfType<GetEventDocumentSignerResponse>();
            result.Data.ShouldNotBeNull();
        }
    }
}
