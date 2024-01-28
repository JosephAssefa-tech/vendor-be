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
using Vennderful.Application.Features.AddOnsCategories.DTOs;
using Vennderful.Application.Features.AddOnsCategories.Handlers.Queries;
using Vennderful.Application.Features.AddOnsCategories.Requests;
using Vennderful.Application.Features.AddOnsCategories.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.AddOnCategoryT
{
    public class GetAddOnsCategoryRequestHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRateStructuresRepository;

        public GetAddOnsCategoryRequestHandlerTest()
        {
            _mockRateStructuresRepository = RepositoryMocks.GetAddOnCategoryRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetRateStructuresTest()
        {
            var handler = new GetAddOnsCategoryRequestHandler(_mockRateStructuresRepository.Object, _mapper);
            var result = await handler.Handle(new GetAddOnsCategoryRequest(), CancellationToken.None);

            result.ShouldBeOfType<GetAddOnsCategoryResponse>();
            result.Data.Count.ShouldBe(2);
        }
    }
}
