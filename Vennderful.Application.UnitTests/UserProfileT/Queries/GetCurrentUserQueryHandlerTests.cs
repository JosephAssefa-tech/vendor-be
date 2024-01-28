using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.Handlers.Queries;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.User.Handlers.Queries;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.UserProfileT.Queries
{
    public class GetCurrentUserQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUserProfileRepository;
        private readonly ILogger<GetCurrentUserQueryHandler> _logger;
        private readonly IUserProfileRepository _userProfileRepository;
        public GetCurrentUserQueryHandlerTests()
        {
            _mockUserProfileRepository = RepositoryMocks.GetUserProfileRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            _logger = loggerFactory.CreateLogger<GetCurrentUserQueryHandler>();
        }

        [Fact]
        public async Task GetUserProfilesTest()
        {
            var handler = new GetCurrentUserQueryHandler(_mockUserProfileRepository.Object, _mapper, _logger, _userProfileRepository);
            var result = await handler.Handle(new GetCurrentUser(), CancellationToken.None);

            result.ShouldBeOfType<GetCurrentUserResponse>();

        }
    }
}
