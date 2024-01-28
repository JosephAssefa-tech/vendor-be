using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Handlers.Commands;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Features.Venue.Commands
{
    public class CreateVenueAccountInformationHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockVenueRepository;
        CreateVenueAccountInformationDto _createVenueDto;
        CreateVenueAccountInformationHandler _createVenueCommandHandler;

        public CreateVenueAccountInformationHandlerTest()
        {
            _mockVenueRepository = RepositoryMocks.GetVenueRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createVenueCommandHandler = new CreateVenueAccountInformationHandler(_mockVenueRepository.Object, _mapper);

            _createVenueDto = new CreateVenueAccountInformationDto
            {
                 Id = Guid.NewGuid(),
                 CompanyName="Depalma",
                 Website ="www.facebook.com",
                 PhoneNumber ="98989343",
                 TypeOfBusinessId = Guid.NewGuid(),
                 Address = new Domain.ValueObjects.Address
                  (
                    "new street",
                    "new city",
                    "new state",
                    "new country",
                    "new zip"
                 )
            };

        }
        [Fact]
        public async Task Should_Create_Venue()
        {
            var result = await _createVenueCommandHandler.Handle(new CreateVenueAccountInformationCommand() { CreateVenueAccountInformationDto = _createVenueDto }
                , CancellationToken.None);

            var venuss = await _mockVenueRepository.Object.VenueAccountInformationRepository.GetAllAsync();

            result.ShouldBeOfType<CreateVenueAccountInformationResponse>();
            result.Success.ShouldBeTrue();
            venuss.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Should_Not_Create_Venue()
        {
            _createVenueDto.CompanyName = null;
            var result = await _createVenueCommandHandler.Handle(new CreateVenueAccountInformationCommand() { CreateVenueAccountInformationDto = _createVenueDto }
                , CancellationToken.None);

            var venus = await _mockVenueRepository.Object.VenueAccountInformationRepository.GetAllAsync();

            result.ShouldBeOfType<CreateVenueAccountInformationResponse>();
            result.Success.ShouldBeFalse();
            venus.Count.ShouldBe(2);
        }
        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _createVenueDto.CompanyName = string.Empty;
            var result = await _createVenueCommandHandler.Handle(new CreateVenueAccountInformationCommand() { CreateVenueAccountInformationDto = _createVenueDto }
                , CancellationToken.None);

            var venus = await _mockVenueRepository.Object.VenueAccountInformationRepository.GetAllAsync();

            result.Errors[0].ShouldBe("Company Name is required.");
        }

    }
}
