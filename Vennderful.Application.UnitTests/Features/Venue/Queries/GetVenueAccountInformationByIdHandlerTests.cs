using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Events.Handlers.Queries;
using Vennderful.Application.Features.Events.Requests;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.User.Handlers.Queries;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Features.UserRoles.Handlers.Commands;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.VenueAccount.Handlers.Commands;
using Vennderful.Application.Features.VenueAccount.Handlers.Queries;
using Vennderful.Application.Features.VenueAccount.Requests;
using Vennderful.Application.Features.VenueAccount.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.Features.Venue.Queries
{
    public class GetVenueAccountInformationByIdHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockVenueRepository;
        private readonly ILogger<GetVenueAccountInformationByIdHandler> _logger;

        public GetVenueAccountInformationByIdHandlerTests()
        {
            _mockVenueRepository = RepositoryMocks.GetVenueRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            var loggerFactory = LoggerFactory.Create(c => c.AddConsole());
            _logger = loggerFactory.CreateLogger<GetVenueAccountInformationByIdHandler>();
        }

        [Fact]
        public async Task Should_Return_Venue()
        {
            var handler = new GetVenueAccountInformationByIdHandler(_mockVenueRepository.Object, _mapper, _logger);
            var result = await handler.Handle(new GetVenueAccountInformationByIdQuery() { CompanyId = Guid.Parse("78398392-0279-4805-bdd8-438bbb6d6324") }, CancellationToken.None);

            result.ShouldBeOfType<GetVenueAccountInformationResponse>();
            result.Success.ShouldBeTrue();
        }
    }
}
