using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.VenueProfile.Handlers.Commands;
using Vennderful.Application.Features.VenueProfile.Requests;
using Vennderful.Application.Features.VenueProfile.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.VenuPublicProfile.Commands
{
    public class UpdateVenuePublicProfileHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockPublicProfileRepository;
        UpdateVenuePublicProfileDTO _updateVenuePublicProfileDTO;
        UpdateVenuePublicProfileHandler _updateVenuePublicProfileHandler;

        public UpdateVenuePublicProfileHandlerTest()
        {
            _mockPublicProfileRepository = RepositoryMocks.GetVenuePublicProfileRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _updateVenuePublicProfileHandler = new UpdateVenuePublicProfileHandler(_mockPublicProfileRepository.Object, _mapper);

            _updateVenuePublicProfileDTO = new UpdateVenuePublicProfileDTO
            {
                ProfilePictureUrl = "updated.png",
                ProfileDescription = "updated profile description",
                VenueAccountInformationId = Guid.Parse("ba57d2dd-c943-4c4b-b8fd-d30fe675d9db")
            };
        }
        [Fact]
        public async Task Should_Update_VenuePublicProfile()
        {
            var result = await _updateVenuePublicProfileHandler.Handle(new UpdateVenuePublicProfileCommand() { UpdateVenuePublicProfileDTO = _updateVenuePublicProfileDTO }
                , CancellationToken.None);

            var pubProfiles = await _mockPublicProfileRepository.Object.VenuePublicProfileRepository.GetAllAsync();

            result.ShouldBeOfType<UpdateVenuePublicProfileResponse>();
            result.Success.ShouldBeTrue();
            pubProfiles.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Not_Update_VenuePublicProfile()
        {
            _updateVenuePublicProfileDTO.VenueAccountInformationId = Guid.Empty;
            var result = await _updateVenuePublicProfileHandler.Handle(new UpdateVenuePublicProfileCommand() { UpdateVenuePublicProfileDTO = _updateVenuePublicProfileDTO }
                , CancellationToken.None);

            var pubProfiles = await _mockPublicProfileRepository.Object.VenuePublicProfileRepository.GetAllAsync();

            result.ShouldBeOfType<UpdateVenuePublicProfileResponse>();
            result.Success.ShouldBeFalse();
            pubProfiles.Count.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _updateVenuePublicProfileDTO.VenueAccountInformationId = Guid.Empty;
            var result = await _updateVenuePublicProfileHandler.Handle(new UpdateVenuePublicProfileCommand() { UpdateVenuePublicProfileDTO = _updateVenuePublicProfileDTO }
                , CancellationToken.None);

            var pubProfiles = await _mockPublicProfileRepository.Object.VenuePublicProfileRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Venue Account Information Id is required.");
        }
    }
}
