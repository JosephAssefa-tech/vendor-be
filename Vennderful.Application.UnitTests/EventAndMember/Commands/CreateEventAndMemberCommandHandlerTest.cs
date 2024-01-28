using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventAndMember.DTO;
using Vennderful.Application.Features.EventAndMember.Handler.Commands;
using Vennderful.Application.Features.EventAndMember.Requests;
using Vennderful.Application.Features.EventAndMember.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventAndMember.Commands
{
    public class CreateEventAndMemberCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventAndMemberRepository;
        private readonly CreateEventAndMemberDTO _createEventAndMemberDTO ;
        private readonly CreateEventAndMemberCommandHandler _createEventAndMemberCommandHandler;

        public CreateEventAndMemberCommandHandlerTest()
        {
            _mockEventAndMemberRepository = RepositoryMocks.GetEventAndMembersRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventAndMemberCommandHandler = new CreateEventAndMemberCommandHandler(_mockEventAndMemberRepository.Object, _mapper);

            _createEventAndMemberDTO = new CreateEventAndMemberDTO
            {
               EventId = Guid.NewGuid(),
               IsActive = true,
               MemberId= Guid.NewGuid(),
            };
        }

        [Fact]
        public async Task Should_Create_EventAndMember()
        {
            var result = await _createEventAndMemberCommandHandler.Handle(new CreateEventAndMemberCommand() { CreateEventAndMemberDTO = _createEventAndMemberDTO }
                , CancellationToken.None);

            var members = await _mockEventAndMemberRepository.Object.eventAndMemberRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventAndMemberResponse>();
            result.Success.ShouldBeTrue();
            members.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Not_Create_EventAndMember()
        {
            _createEventAndMemberDTO.EventId = Guid.Empty;
            var result = await _createEventAndMemberCommandHandler.Handle(new CreateEventAndMemberCommand() { CreateEventAndMemberDTO = _createEventAndMemberDTO }
                , CancellationToken.None);

            var members = await _mockEventAndMemberRepository.Object.eventAndMemberRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventAndMemberResponse>();
            result.Success.ShouldBeFalse();
            members.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createEventAndMemberDTO.MemberId = Guid.Empty;
            var result = await _createEventAndMemberCommandHandler.Handle(new CreateEventAndMemberCommand() { CreateEventAndMemberDTO = _createEventAndMemberDTO }
                , CancellationToken.None);

            var members = await _mockEventAndMemberRepository.Object.eventAndMemberRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Member Id is required.");
        }
    }
}
