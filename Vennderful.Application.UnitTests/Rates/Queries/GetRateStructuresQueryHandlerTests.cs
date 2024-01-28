using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.Handlers.Queries;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.RateStructure.DTOs;
using Vennderful.Application.Features.RateStructure.Handlers.Queries;
using Vennderful.Application.Features.RateStructure.Requests;
using Vennderful.Application.Features.RateStructure.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.RateStructure.Queries
{
    public class GetRateStructuresQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRateStructuresRepository;

        public GetRateStructuresQueryHandlerTests()
        {
            _mockRateStructuresRepository = RepositoryMocks.GetRateStructureRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetRateStructuresTest()
        {
            var handler = new GetRateStructuresQueryHandler(_mockRateStructuresRepository.Object, _mapper);
            var result = await handler.Handle(new GetRateStructuresRequest(), CancellationToken.None);

            result.ShouldBeOfType<GetRateStructuresResponse>();
            result.Data.Count.ShouldBe(2);
        }
    }
}
