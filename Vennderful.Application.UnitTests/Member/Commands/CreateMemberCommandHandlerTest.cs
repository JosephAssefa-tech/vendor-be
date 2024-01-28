using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Member.DTO;
using Vennderful.Application.Features.Member.Handler.Commands;
using Vennderful.Application.Features.Member.Requests;
using Vennderful.Application.Features.Member.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.UnitTests.Member.Commands
{
    public class CreateMemberCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockMembertRepository;
        private readonly CreateMemberDTO _createMemberDTO;
        private readonly CreateMemberCommandHandler _createMemberCommandHandler;

        public CreateMemberCommandHandlerTest()
        {
            _mockMembertRepository = RepositoryMocks.GetMembersRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createMemberCommandHandler = new CreateMemberCommandHandler(_mockMembertRepository.Object, _mapper);

            _createMemberDTO = new CreateMemberDTO
            {
                Email = "user.member2@yahoo.com",
                FirstName = "",
                LastName = "",
                Gender = Gender.Female,
                JobTitle = "Bartender",
                ProfilePicture = "",
                UserRole = "Admin",
            };
        }

        [Fact]
        public async Task Should_Create_Member()
        {
            var result = await _createMemberCommandHandler.Handle(new CreateMemberCommand() { CreateMemberDTO = _createMemberDTO }
                , CancellationToken.None);

            var members = await _mockMembertRepository.Object.memberRepository.GetAllAsync();

            result.ShouldBeOfType<CreateMemberResponse>();
            result.Success.ShouldBeTrue();
            members.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Not_Create_Member()
        {
            _createMemberDTO.Email = string.Empty;
            var result = await _createMemberCommandHandler.Handle(new CreateMemberCommand() { CreateMemberDTO = _createMemberDTO }
                , CancellationToken.None);

            var members = await _mockMembertRepository.Object.memberRepository.GetAllAsync();

            result.ShouldBeOfType<CreateMemberResponse>();
            result.Success.ShouldBeFalse();
            members.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createMemberDTO.Email = string.Empty;
            var result = await _createMemberCommandHandler.Handle(new CreateMemberCommand() { CreateMemberDTO = _createMemberDTO }
                , CancellationToken.None);

            var members = await _mockMembertRepository.Object.memberRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Email is required.");
        }
    }
}
