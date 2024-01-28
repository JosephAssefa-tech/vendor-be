using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.User.Handlers.Queries;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.UserProfileT.Queries
{
    public class GetUserInviteesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUserProfileRepository;

        public GetUserInviteesQueryHandlerTests()
        {
            _mockUserProfileRepository = RepositoryMocks.GetUserProfileRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetUserInvitesTest()
        {
            var handler = new GetUserInvitesQueryHandler(_mockUserProfileRepository.Object, _mapper);
            var result = await handler.Handle(new GetUserInvitesRequest() { CompanyId = "3fa85f64-5717-4562-b3fc-2c963f66afb6" }, CancellationToken.None);

            result.ShouldBeOfType<GetUserInvitesResponse>();
            result.Data.Count.ShouldBe(1);
        }
    }
}
