using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Handlers.Commands;
using Vennderful.Application.Features.VenueProfile.Requests;
using Vennderful.Application.Features.VenueProfile.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.UnitTests.VenuPublicProfile.Commands
{
    public class CurateVenuePublicProfileHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockPublicProfileRepository;
        CurateVenuePublicProfileDto _curateVenuePublicProfileDto;
        CurateVenuePublicProfileHandler _curateVenuePublicProfileHandler;

        public CurateVenuePublicProfileHandlerTest()
        {
            _mockPublicProfileRepository = RepositoryMocks.GetVenuePublicProfileRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _curateVenuePublicProfileHandler = new CurateVenuePublicProfileHandler(_mockPublicProfileRepository.Object, _mapper);

            _curateVenuePublicProfileDto = new CurateVenuePublicProfileDto
            {
                //Id = Guid.NewGuid(),
                // SocialProfilesId = Guid.NewGuid(),
                ProfilePictureUrl = "",
                ProfileDescription = "Test public profile description3",
                 VenueAccountInformationId = Guid.Parse("ba57d2dd-c943-4c4b-b8fd-d30fe675d9db")
            };

        }
        [Fact]
        public async Task Should_Create_VenuePublicProfile()
        {
            var result = await _curateVenuePublicProfileHandler.Handle(new CurateVenuePublicProfileCommand() { CurateVenuePublicProfileDto = _curateVenuePublicProfileDto }
                , CancellationToken.None);

            var pubProfiles = await _mockPublicProfileRepository.Object.VenuePublicProfileRepository.GetAllAsync();

            result.ShouldBeOfType<CurateVenuePublicProfileResponse>();
            result.Success.ShouldBeTrue();
            pubProfiles.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Should_Not_Create_VenuePublicProfile()
        {
            _curateVenuePublicProfileDto.VenueAccountInformationId = Guid.Empty;
            var result = await _curateVenuePublicProfileHandler.Handle(new CurateVenuePublicProfileCommand() { CurateVenuePublicProfileDto = _curateVenuePublicProfileDto }
                , CancellationToken.None);

            var pubProfiles = await _mockPublicProfileRepository.Object.VenuePublicProfileRepository.GetAllAsync();

            result.ShouldBeOfType<CurateVenuePublicProfileResponse>();
            result.Success.ShouldBeFalse();
            pubProfiles.Count.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Show_Error_Message()
        {
           _curateVenuePublicProfileDto.VenueAccountInformationId = Guid.Empty;
            var result = await _curateVenuePublicProfileHandler.Handle(new CurateVenuePublicProfileCommand() { CurateVenuePublicProfileDto = _curateVenuePublicProfileDto }
                , CancellationToken.None);

            var pubProfiles = await _mockPublicProfileRepository.Object.VenuePublicProfileRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Venue Account Information Id is required.");
        }

    }
}
