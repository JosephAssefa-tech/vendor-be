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
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Application.Features.UserRoles.Handlers.Commands;
using Vennderful.Application.Features.UserRoles.Requests;
using Vennderful.Application.Features.UserRoles.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.UnitTests.UserRoleT.Commands
{
    public class AddUserRoleCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUserRoleRepository;
        private readonly AddUserRoleDTO _addUserRoleDTO;
        private readonly AddUserRoleCommandHandler _addUserRoleCommandHandler;
        private readonly ILogger<AddUserRoleCommandHandler> _logger;


        public AddUserRoleCommandHandlerTests()
        {
            _mockUserRoleRepository = RepositoryMocks.GetUserRoleRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            _logger = loggerFactory.CreateLogger<AddUserRoleCommandHandler>();

            _addUserRoleCommandHandler = new AddUserRoleCommandHandler(_mockUserRoleRepository.Object, _mapper, _logger);

            _addUserRoleDTO = new AddUserRoleDTO
            {
                Id = Guid.NewGuid(),
                UserRoleType = UserRoleType.Venue
                
            };
        }

        [Fact]
        public async Task Should_Add_UserRole()
        {
            var result = await _addUserRoleCommandHandler.Handle(new AddUserRoleCommand() { AddUserRoleDTO = _addUserRoleDTO }
                , CancellationToken.None);

            var users = await _mockUserRoleRepository.Object.UserRoleRepository.GetAllAsync();

            result.ShouldBeOfType<AddUserRoleResponse>();

            users.Count.ShouldBe(2);
        }

       
        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _addUserRoleDTO.CompanyId = Guid.Empty;
            var result = await _addUserRoleCommandHandler.Handle(new AddUserRoleCommand() { AddUserRoleDTO = _addUserRoleDTO }
                , CancellationToken.None);

            var users = await _mockUserRoleRepository.Object.UserRoleRepository.GetAllAsync();
            result.Errors[0].ShouldBe("Company Id is required.");

        }
    }
}
