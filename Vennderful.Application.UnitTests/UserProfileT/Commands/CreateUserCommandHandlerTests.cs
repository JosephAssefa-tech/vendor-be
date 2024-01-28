using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Handlers.Commands;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.User.Handlers.Commands;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.UserProfileT.Commands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUserProfileRepository;
        private readonly UserRegisterDto _registerUserrDTO;
        private readonly CreateUserCommandHandler _createUserCommandHandler;
        private readonly ILogger<CreateUserCommandHandler> _logger;
       

        public CreateUserCommandHandlerTests()
        {
            _mockUserProfileRepository = RepositoryMocks.GetUserProfileRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            _logger = loggerFactory.CreateLogger<CreateUserCommandHandler>();

            _createUserCommandHandler = new CreateUserCommandHandler(_mockUserProfileRepository.Object, _mapper, _logger);

            _registerUserrDTO = new UserRegisterDto
            {
                UserId = Guid.NewGuid(),
                Email= "test@testemail.com",
                Password = "Test*P123",
                FirstName = "UserFName",
                LastName = "UserLastName",
                
            };
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var result = await _createUserCommandHandler.Handle(new CreateUserCommand() { UserRegisterDto = _registerUserrDTO }
                , CancellationToken.None);

            var users = await _mockUserProfileRepository.Object.UserProfileRepository.GetAllAsync();

            result.ShouldBeOfType<CreateUserResponse>();
            
            users.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Should_Not_Create_User()
        {
            _registerUserrDTO.Email = null;
            var result = await _createUserCommandHandler.Handle(new CreateUserCommand() { UserRegisterDto = _registerUserrDTO }
                , CancellationToken.None);

            var customers = await _mockUserProfileRepository.Object.UserProfileRepository.GetAllAsync();

            result.ShouldBeOfType<CreateUserResponse>();
            customers.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _registerUserrDTO.Email = string.Empty;
            var result = await _createUserCommandHandler.Handle(new CreateUserCommand() { UserRegisterDto = _registerUserrDTO }
                , CancellationToken.None);

            var users = await _mockUserProfileRepository.Object.UserProfileRepository.GetAllAsync();
            result.Errors[0].ShouldBe("Email is required.");

        }
    }
}
