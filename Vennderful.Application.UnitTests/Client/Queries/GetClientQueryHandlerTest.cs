using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Handlers.Queries;
using Vennderful.Application.Features.Client.Requests;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Client.Queries
{
    public class GetClientQueryHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockClientRepository;
        private readonly GetClientQueryHandler _getClientHandler;

        public GetClientQueryHandlerTest()
        {
            _mockClientRepository = RepositoryMocks.GetClientRepository();
            _getClientHandler = new GetClientQueryHandler(_mockClientRepository.Object);
        }

        [Fact]
        public async Task Should_Get_Client()
        {
            var request = new GetClientRequest() { ClientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6" };
            var client = await _mockClientRepository.Object.clientRepository.GetClientById(request.ClientId);

            var result = await _getClientHandler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<GetClientResponse>();
            result.Success.ShouldBeTrue();
        }
    }
}
