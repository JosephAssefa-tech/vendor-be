using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Handlers.Commands;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.UnitTests.Features.Venue.Commands
{
    public class CompleteVenueCreationHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockVenueRepository;
        readonly CompleteVenueCreationDTO _completeVenueCreationDto;
        readonly CompleteVenueCreationHandler _completeVenueCreationHandler;

        public CompleteVenueCreationHandlerTest()
        {
            _mockVenueRepository = RepositoryMocks.GetVenueRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _completeVenueCreationHandler = new CompleteVenueCreationHandler(_mockVenueRepository.Object, _mapper);

            _completeVenueCreationDto = new CompleteVenueCreationDTO()
            {
                CompanyName = "Vennderful",
                CompanyId=Guid.Parse("69398392-0279-4805-bdd8-438bbb6d6324"),
                Status = Domain.Enums.CompanyProfileStatus.Completed,
            };
        }

        [Fact]
        public async Task Should_Complete_Venue()
        {
            var result = await _completeVenueCreationHandler.Handle(new CompleteVenueCreationCommand() { CompleteVenueCreationDTO = _completeVenueCreationDto }
                , CancellationToken.None);

            var profile = await _mockVenueRepository.Object.VenueAccountInformationRepository.GetVenueByCompanyName(result.Data.CompanyName, result.Data.CompanyId);

            result.ShouldBeOfType<CompleteVenueCreationResponse>();
            result.Success.ShouldBeTrue();
            profile.Status.ShouldBe(Domain.Enums.CompanyProfileStatus.Completed);
        }

        [Fact]
        public async Task Should_Not_Complete_Venue()
        {
            _completeVenueCreationDto.CompanyName = "Test Company";
            var result = await _completeVenueCreationHandler.Handle(new CompleteVenueCreationCommand() { CompleteVenueCreationDTO = _completeVenueCreationDto }
                , CancellationToken.None);

            result.ShouldBeOfType<CompleteVenueCreationResponse>();
            result.Success.ShouldBeFalse();
        }
        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _completeVenueCreationDto.CompanyName = null;
            var result = await _completeVenueCreationHandler.Handle(new CompleteVenueCreationCommand() { CompleteVenueCreationDTO = _completeVenueCreationDto }
                , CancellationToken.None);

            result.Errors[0].ShouldBe("Company Name is required.");
            result.Success.ShouldBeFalse();
        }
    }
}
