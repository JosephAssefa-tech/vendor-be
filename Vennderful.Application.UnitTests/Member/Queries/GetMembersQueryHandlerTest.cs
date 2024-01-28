using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Member.DTO;
using Vennderful.Application.Features.Member.Handler.Queries;
using Vennderful.Application.Features.Member.Requests;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Member.Queries
{
    public class GetMembersQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockMembersRepository;

        public GetMembersQueryHandlerTest()
        {
            _mockMembersRepository = RepositoryMocks.GetMembersRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResponseWithData()
        {
            // Arrange
            var request = new GetMembersRequest { CompanyId = Guid.Parse("17cc8d88-3dc7-481d-9f8b-1af76a9f7b23") };
            var cancellationToken = CancellationToken.None;

            // Act
            var handler = new GetMembersQueryHandler(_mockMembersRepository.Object, _mapper);
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            // response.Success.ShouldBeTrue();
            response.Data.ShouldBeOfType<List<ListMembersDTO>>();
        }

        [Fact]
        public async Task Handle_ExceptionThrown_ReturnsErrorResponseWithMessageAndEmptyData()
        {
            // Arrange
            var request = new GetMembersRequest { CompanyId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d") };
            var cancellationToken = CancellationToken.None;

            // Simulate an exception being thrown
            _mockMembersRepository.Setup(repo => repo.memberRepository.GetAllAsync())
                .ThrowsAsync(new Exception("An error occurred while getting members."));

            // Act
            var handler = new GetMembersQueryHandler(_mockMembersRepository.Object, _mapper);
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeFalse();
            response.Message.ShouldBe("Something went wrong.");
            response.Data.ShouldBeEmpty();
            response.Errors.ShouldNotBeEmpty();
        }
    }
}
